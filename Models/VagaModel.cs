using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeVagas.Models
{
    public class VagaModel
    {
        [Required(ErrorMessage = "Informe o andar da vaga")]
        public int Andar { get; set; }
        [Required(ErrorMessage = "Informe o corredor da vaga")]
        public int Corredor { get; set; }
        [Required(ErrorMessage = "Informe o número da vaga")]
        public int Numero { get; set; }
        [Required(ErrorMessage = "Selecione o porte do veículo")]
        public string PorteVeiculo { get; set; }
    }
}
