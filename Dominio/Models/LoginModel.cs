using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Models
{
    public class LoginModel
    {
        [Key]       
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o email do usuário")]
        [EmailAddress(ErrorMessage = "O email do usuário não é um email valido!")]
        public string? Email { get; set; }        

        [Required(ErrorMessage = "Digite a senha do usuário")]
        [DataType(DataType.Password)]
        public string? Senha { get; set; }
    }
}
