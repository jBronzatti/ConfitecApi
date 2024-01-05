using Confitec.Models;
using Confitec.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Confitec.Dtos
{
    public class UsuarioCreateDto
    {
        [Required(ErrorMessage = "Nome é necessário")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Sobrenome é necessário")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Email é necessário")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [DateValidation(ErrorMessage = "Data de nascimento inválida")]
        [Required(ErrorMessage = "Data de nascimento é necessária")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Escolaridade é necessária")]
        [EnumDataType(typeof(Escolaridade))]
        public Escolaridade Escolaridade { get; set; }
    }
}
