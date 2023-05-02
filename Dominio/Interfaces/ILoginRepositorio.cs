using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface ILoginRepositorio
    {
        Task<bool> Login(string email, string password);
        Task<LoginModel> GetByEmail(string email, string password);
    }
}
