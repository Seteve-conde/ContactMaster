using Dominio.Interfaces;
using Dominio.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http.Extensions;
using ContactMaster.Services;
using System;

namespace ContactMasterService.Services
{
    public class SessaoService : ISessaoService
    {
        private readonly IHttpContextAccessor _httpContext;

        public SessaoService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext)); 
        }

        public UsuarioModel BuscarSessaoUsuario()
        {
            string sessaoUsuario = _httpContext.HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuario)) return null;

            return JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
        }

        public void CriarSessaoUsuario(UsuarioModel usuario)
        {
            string valor = JsonConvert.SerializeObject(usuario, Formatting.Indented);

            _httpContext.HttpContext.Session.SetString("sessaoUsuarioLogado", valor);
        }

        public void RemoverSessaoUsuario()
        {
            _httpContext.HttpContext.Session.Remove("sessaoUsuarioLogado");
        }
    }
}
