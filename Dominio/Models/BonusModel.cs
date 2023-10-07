using System.ComponentModel.DataAnnotations;

namespace Dominio.Models
{
    public class BonusModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do contato")]
        [StringLength(maximumLength: 100, ErrorMessage = "O nome é muito longo")]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Selecionado { get; set; }
        public int? UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
