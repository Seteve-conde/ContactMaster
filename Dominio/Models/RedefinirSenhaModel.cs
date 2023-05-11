using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Models
{
    public class RedefinirSenhaModel
    {
        [Key]
        [Required(ErrorMessage = "Digite o email do usuário")]
        [EmailAddress(ErrorMessage = "O email do usuário não é um email valido!")]
        public string Email { get; set; }        
    }
}
