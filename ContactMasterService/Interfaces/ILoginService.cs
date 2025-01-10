using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMaster.Services
{
    public interface ILoginService
    {
        Task<bool> Login(string email, string password);
    }
}
