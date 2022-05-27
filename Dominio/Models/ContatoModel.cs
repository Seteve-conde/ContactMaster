using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Dominio.Models
{
    public class ContatoModel
    {
        [Key]
        public int Id { get; set; }

        [Required (ErrorMessage = "O campo nome é obrigatório")]
        [StringLength(maximumLength:100, ErrorMessage = "O nome é muito longo")]
        [DisplayName("Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo e-mail é obrigatório")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        public string Celular { get; set; }
    }
}
