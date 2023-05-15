using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Models
{
    public class AlterarSenhaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite a senha atual do usuário")]
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = "Digite a nova senha do usuário")]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "Confirme a nova senha do usuário")]
        [Compare("NovaSenha", ErrorMessage = "Senha não confere com a nova senha")]
        public string ConfirmarSenha { get; set; }
    }
}
