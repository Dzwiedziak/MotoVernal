using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Decorators
{
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var comparisonProperty = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (comparisonProperty == null)
            {
                return new ValidationResult($"Unknown property: {_comparisonProperty}");
            }

            var comparisonValue = comparisonProperty.GetValue(validationContext.ObjectInstance);

            if (value is DateTime endDate && comparisonValue is DateTime startDate && endDate <= startDate)
            {
                return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} must be later than {_comparisonProperty}.");
            }

            return ValidationResult.Success!;
        }
    }

}
