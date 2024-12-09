using System.ComponentModel.DataAnnotations;


namespace BusinessLogic.Decorators
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateTime && dateTime <= DateTime.Now)
            {
                return new ValidationResult(ErrorMessage ?? "Date must be in the future.");
            }
            return ValidationResult.Success!;
        }
    }

}
