using Dominio.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ContactMaster.ViewComponet
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessaoDoUsuario = await GetSessionStringAsync("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoDoUsuario)) return null;

            UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoDoUsuario);

            return View(usuario);
        }

        private Task<string> GetSessionStringAsync(string key)
        {
            return Task.Run(() => HttpContext.Session.GetString(key));
        }
    }
}
