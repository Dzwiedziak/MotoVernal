using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace MotoVendor
{
    public static class ListFilter
    {
        public static List<T> FilterList<T>(List<T> dbList, Dictionary<string, HashSet<string>> filters)
        {
            if (dbList == null || filters == null || !filters.Any())
                return dbList;

            var parameter = Expression.Parameter(typeof(T), "x");
            Expression combinedExpression = null;

            foreach (var filter in filters)
            {
                Expression keyExpression = null;

                foreach (var filterValue in filter.Value)
                {
                    string[] keyParts = filter.Key.Split(new[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                    string propertyName = keyParts[0];
                    string comparisonType = keyParts.Length > 1 ? keyParts[1] : "eq";

                    var property = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (property == null)
                        throw new ArgumentException($"Property '{propertyName}' does not exist on type '{typeof(T).Name}'");

                    var propertyAccess = Expression.Property(parameter, property);

                    Expression comparison;
                    if (property.PropertyType.IsEnum)
                    {
                        var enumValue = Enum.Parse(property.PropertyType, filterValue, true);
                        var value = Expression.Constant(enumValue, property.PropertyType);

                        comparison = comparisonType switch
                        {
                            "lte" => Expression.LessThanOrEqual(propertyAccess, value),
                            "lt" => Expression.LessThan(propertyAccess, value),
                            "gte" => Expression.GreaterThanOrEqual(propertyAccess, value),
                            "gt" => Expression.GreaterThan(propertyAccess, value),
                            "neq" => Expression.NotEqual(propertyAccess, value),
                            _ => Expression.Equal(propertyAccess, value),
                        };
                    }
                    else if (property.PropertyType == typeof(DateTime))
                    {
                        if (!DateTime.TryParse(filterValue, out DateTime dateValue))
                            throw new ArgumentException($"Invalid date format for filter value '{filterValue}' for property '{propertyName}'");

                        var value = Expression.Constant(dateValue, typeof(DateTime));

                        comparison = comparisonType switch
                        {
                            "lte" => Expression.LessThanOrEqual(propertyAccess, value),
                            "lt" => Expression.LessThan(propertyAccess, value),
                            "gte" => Expression.GreaterThanOrEqual(propertyAccess, value),
                            "gt" => Expression.GreaterThan(propertyAccess, value),
                            "neq" => Expression.NotEqual(propertyAccess, value),
                            _ => Expression.Equal(propertyAccess, value),
                        };
                    }
                    else
                    {
                        var value = Expression.Constant(Convert.ChangeType(filterValue, property.PropertyType));

                        comparison = comparisonType switch
                        {
                            "lte" => Expression.LessThanOrEqual(propertyAccess, value),
                            "lt" => Expression.LessThan(propertyAccess, value),
                            "gte" => Expression.GreaterThanOrEqual(propertyAccess, value),
                            "gt" => Expression.GreaterThan(propertyAccess, value),
                            "neq" => Expression.NotEqual(propertyAccess, value),
                            _ => Expression.Equal(propertyAccess, value),
                        };
                    }

                    keyExpression = keyExpression == null
                        ? comparison
                        : Expression.OrElse(keyExpression, comparison);
                }
                if (keyExpression != null)
                {
                    combinedExpression = combinedExpression == null
                        ? keyExpression
                        : Expression.AndAlso(combinedExpression, keyExpression);
                }
            }

            if (combinedExpression == null)
                return dbList;

            var lambda = Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);
            return dbList.AsQueryable().Where(lambda).ToList();
        }
        public static List<T> SortList<T>(List<T> dbList, string filter)
        {
            if (dbList == null || string.IsNullOrWhiteSpace(filter))
                return dbList;

            string[] filterParts = filter.Split(new[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
            if (filterParts.Length == 0)
                throw new ArgumentException("Invalid filter format. Expected format: 'Property[asc]' or 'Property[desc]'.");

            string propertyName = filterParts[0];
            string sortOrder = filterParts.Length > 1 ? filterParts[1].ToLower() : "asc";

            var property = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (property == null)
                throw new ArgumentException($"Property '{propertyName}' does not exist on type '{typeof(T).Name}'.");

            var parameter = Expression.Parameter(typeof(T), "x");
            var propertyAccess = Expression.Property(parameter, property);

            var keySelector = Expression.Lambda<Func<T, object>>(
                Expression.Convert(propertyAccess, typeof(object)),
                parameter
            );

            return sortOrder switch
            {
                "asc" => dbList.AsQueryable().OrderBy(keySelector).ToList(),
                "desc" => dbList.AsQueryable().OrderByDescending(keySelector).ToList(),
                _ => throw new ArgumentException($"Invalid sort order '{sortOrder}'. Expected 'asc' or 'desc'.")
            };
        }
        public static List<T> GetPaginatedList<T>(List<T> elements, int pageIndex, int pageSize)
        {
            var paginatedElements = elements.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return paginatedElements;
        }
    }
}
