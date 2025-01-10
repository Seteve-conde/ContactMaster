using Dominio.Models;

namespace ContactMaster.Services
{
    public interface ISessaoService    {
        void CriarSessaoUsuario(UsuarioModel usuario);
        void RemoverSessaoUsuario();
        UsuarioModel BuscarSessaoUsuario();
    }
}
