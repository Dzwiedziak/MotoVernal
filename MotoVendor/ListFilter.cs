using System.Linq.Expressions;
using System.Reflection;

namespace MotoVendor
{
    public static class ListFilter
    {
        public static List<T> FilterList<T>(List<T> dbList, Dictionary<string, string> filters)
        {
            if (dbList == null || filters == null || !filters.Any())
                return dbList;

            var parameter = Expression.Parameter(typeof(T), "x");
            Expression combinedExpression = null;

            foreach (var filter in filters)
            {
                string[] keyParts = filter.Key.Split(new[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                string propertyName = keyParts[0];
                string comparisonType = keyParts.Length > 1 ? keyParts[1] : "eq";

                var property = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (property == null)
                    throw new ArgumentException($"Property '{propertyName}' does not exist on type '{typeof(T).Name}'");

                var propertyAccess = Expression.Property(parameter, property);
                var value = Expression.Constant(Convert.ChangeType(filter.Value, property.PropertyType));

                Expression comparison = comparisonType switch
                {
                    "lte" => Expression.LessThanOrEqual(propertyAccess, value),
                    "lt" => Expression.LessThan(propertyAccess, value),
                    "gte" => Expression.GreaterThanOrEqual(propertyAccess, value),
                    "gt" => Expression.GreaterThan(propertyAccess, value),
                    "neq" => Expression.NotEqual(propertyAccess, value),
                    _ => Expression.Equal(propertyAccess, value),
                };

                combinedExpression = combinedExpression == null
                    ? comparison
                    : Expression.AndAlso(combinedExpression, comparison);
            }

            if (combinedExpression == null)
                return dbList;

            var lambda = Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);
            return dbList.AsQueryable().Where(lambda).ToList();
        }
    }
}
