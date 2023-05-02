using System;
using System.ComponentModel.DataAnnotations;
using UserEnumPerfil;

namespace Dominio.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do usuário")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o login do usuário")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite o email do usuário")]
        [EmailAddress(ErrorMessage = "O email do usuário não é um email valido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite a senha do usuário")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        public string Perfil { get; set; }
        public UsuariosPerfilEnum PerfilEnum { get; set; }
        public DateTime DataUserCreated { get; set; }
        public DateTime? AtualizationDate { get; set;}
        public bool Selecionado { get; set; }
    }
}
