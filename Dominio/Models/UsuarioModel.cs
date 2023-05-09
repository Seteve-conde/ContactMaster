using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace Dominio.Models
{
    public static class Cripto
    {
        public static string CreateHash(this string value)
        {
            var hash = SHA1.Create();
            var encode = new ASCIIEncoding();
            var array = encode.GetBytes(value);

            array = hash.ComputeHash(array);

            var strHexa = new StringBuilder();

            foreach (var item in array)
            {
                strHexa.Append(item.ToString("x2"));
            }

            return strHexa.ToString();
        }
    }
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
        public UsuariosPerfilEnum? PerfilEnum { get; set; }
        public DateTime DataUserCreated { get; set; }
        public DateTime? AtualizationDate { get; set;}
        public bool Selecionado { get; set; }

        

        public bool SenhaValida(string senha)
        {
            return Senha == senha.CreateHash();
        }

        public void SetSenhaHash()
        {
            Senha = Senha.CreateHash();
        }

        public string GerarNovaSenha() 
        {
            string novaSenha = Guid.NewGuid().ToString().Substring(0,8);
            Senha = novaSenha.CreateHash();
            return novaSenha;
        }

    }
}
