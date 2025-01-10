using ContactMaster.Services;
using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMasterService.Services
{
    public class LoginService : ILoginService
    {

        private readonly ILoginRepositorio _loginRepositorio;

        public LoginService(ILoginRepositorio loginRepositorio)
        {
            _loginRepositorio = loginRepositorio;
        }

        public async Task<bool> Login(string email, string password)
        {
            var user = await _loginRepositorio.GetByEmail(email, password);

            if (user == null || user.Senha != password)
            {
                return false;
            }

            // Autenticação bem-sucedida
            return true;
        }
    }
}

