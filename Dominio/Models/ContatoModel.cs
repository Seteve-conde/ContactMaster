using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

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

        [Required(ErrorMessage = "Digite o celular do contato")]
        public DateTime Data { get; set; }
    }
}
