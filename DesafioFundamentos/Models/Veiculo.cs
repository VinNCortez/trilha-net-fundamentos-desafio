using DesafioFundamentos.validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioFundamentos.Models
{
    internal class Veiculo
    {
        [Required(ErrorMessage = "É necessário informar a placa")]
        [RegularExpression(@"[a-zA-Z]{3}\d[a-zA-Z\d][\d]{2}", ErrorMessage = "A placa é inválida")]
        [PlacaExistsValidation(ErrorMessage = "A placa já foi informada")]
        public string Placa { get; set; }

        public Veiculo(string placa) 
        {
            Placa = placa.ToUpper();
        }
    }
}
