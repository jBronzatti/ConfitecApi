using System.ComponentModel.DataAnnotations;

namespace Confitec.Validations
{
    public class DateValidation : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return "A data não pode ser uma data futura";
        }

        protected override ValidationResult IsValid(object objValue, ValidationContext validationContext)
        {
            var dateValue = objValue as DateTime? ?? new DateTime();

            if (dateValue.Date > DateTime.Now.Date)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return ValidationResult.Success;
        }
    }
}
