using Dominio.Models;


namespace Dominio.Interfaces
{
    public interface IContatoRepositorio
    {
        ContatoModel Adicionar(ContatoModel contato);
    }
}
