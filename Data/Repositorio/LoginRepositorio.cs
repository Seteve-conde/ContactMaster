using DataContext;
using Dominio.Interfaces;
using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Dados.Repositorio
{
    public class LoginRepositorio : ILoginRepositorio
    {
        private readonly BancoContext _bancoContext;

        public LoginRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public async Task<LoginModel> GetByEmail(string email, string password) 
        {
            return await _bancoContext.LoginModels.FirstOrDefaultAsync(u => u.Email == email && u.Senha == password);
        }

        public async Task<bool> Login(string email, string password)
        {
            var emailLogin = await GetByEmail(email, password);

            if (emailLogin.Email == null || emailLogin.Senha != password)
            {
                return false;
            }

            // Autenticação bem-sucedida
            return true;
        }
    }
}
