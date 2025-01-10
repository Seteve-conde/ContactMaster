using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMaster.Services
{
    public interface IEmailService
    {
        Task<bool> Enviar(string email, string assunto, string mensagem);
    }
}
