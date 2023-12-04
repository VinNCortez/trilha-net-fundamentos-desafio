using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DesafioFundamentos.Models;

namespace DesafioFundamentos.validators
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PlacaExistsValidation: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext.Items.TryGetValue("veiculos", out object veiculosObject))
            {
                return !((List<Veiculo>)veiculosObject).Exists(v => v.Placa == ((string)value))
                    ? ValidationResult.Success : new ValidationResult(ErrorMessage);
            }
            else
            {
                throw new ArgumentException("O parametro veiculos não foi informado");
            }
        }
    }
}
