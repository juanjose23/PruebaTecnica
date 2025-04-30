using System;
using System.ComponentModel.DataAnnotations;
namespace PruebaTecnica.Validations
{
    public class EnumValidoAttribute: ValidationAttribute
    {
        // / <summary>
        /// Valida si el valor es un valor válido de la enumeración.
        /// </summary>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
            // Verifica si el valor es nulo o no es un valor válido de la enumeración
            if (value == null)
                {
                    return new ValidationResult("El valor no puede ser nulo.");
                }
            // Verifica si el valor es un tipo de enumeración
            if (!Enum.IsDefined(value.GetType(), value))
                {
                    return new ValidationResult($"El valor '{value}' no es un valor válido de la enumeración '{value.GetType().Name}'.");
                }
                return ValidationResult.Success;
            }
        

    }
    
}
