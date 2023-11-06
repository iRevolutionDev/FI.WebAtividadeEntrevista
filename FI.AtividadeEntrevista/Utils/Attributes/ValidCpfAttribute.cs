using System.ComponentModel.DataAnnotations;

namespace FI.AtividadeEntrevista.Utils.Attributes
{
    public class ValidCpfAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "CPF inválido";
        private const string CpfRegex = @"^\d{3}\.\d{3}\.\d{3}-\d{2}$";

        public ValidCpfAttribute()
        {
            ErrorMessage = DefaultErrorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult(ErrorMessage);

            if (!new RegularExpressionAttribute(CpfRegex).IsValid(value.ToString()))
                return new ValidationResult(ErrorMessage);

            return !CpfConverter.IsValid(value.ToString())
                ? new ValidationResult(ErrorMessage)
                : ValidationResult.Success;
        }
    }
}