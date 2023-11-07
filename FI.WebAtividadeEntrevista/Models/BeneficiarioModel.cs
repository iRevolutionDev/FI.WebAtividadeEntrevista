using System.ComponentModel.DataAnnotations;
using FI.AtividadeEntrevista.Utils.Attributes;

namespace WebAtividadeEntrevista.Models
{
    public class BeneficiarioModel
    {
        public long Id { get; set; }

        [Required] public long IdCliente { get; set; }

        [Required] public string Nome { get; set; }

        [Required] [MaxLength(14)] [ValidCpf] public string CPF { get; set; }
    }
}