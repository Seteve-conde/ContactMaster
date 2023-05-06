using ContactMasterService;
using Dominio.Models;

namespace ContactMaster.Helper
{
    public class Cripto : UsuarioModel
    {
        public void SetSenhaHash()
        {
            Senha = Senha.CreateHash();
        }
    }
}
