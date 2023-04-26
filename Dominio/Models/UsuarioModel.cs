using System;
using UserEnumPerfil;

namespace Dominio.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Perfil { get; set; }
        public string Senha { get; set; }
        public UsuariosPerfilEnum PerfilEnum { get; set; }
        public DateTime DataUserCreated { get; set; }
        public DateTime? AtualizationDate { get; set;}
        public bool Selecionado { get; set; }
    }
}
