using Confitec.Validations;
using System.ComponentModel.DataAnnotations;

namespace Confitec.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é necessário")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Sobrenome é necessário")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Email é necessário")]
        public string Email { get; set; }

        [DateValidation]
        [Required(ErrorMessage = "Data de nascimento é necessária")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Escolaridade é necessária")]
        public Escolaridade Escolaridade { get; set; }
    }

    public enum Escolaridade
    {
        Infantil,
        Fundamental,
        Médio,
        Superior
    }
}
