using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Models
{
    public class ContatoModel
    {
        [Key]
        public int Id { get; set; }

        [Required (ErrorMessage = "Digite o nome do contato")]
        [StringLength(maximumLength:100, ErrorMessage = "O nome é muito longo")]
        //[DisplayName("Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o e-mail do contato")]
        [EmailAddress(ErrorMessage = "O E-mail informado não é valido!")]
        //[DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite o celular do contato")]
        [Phone(ErrorMessage = "O celular informado não é valido!")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "Esta data não é valida")]
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public bool Selecionado { get; set; }
        public int? UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
