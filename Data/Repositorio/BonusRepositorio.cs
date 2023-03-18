using DataContext;
using Dominio.Interfaces;
using Dominio.Models;
using System.Collections.Generic;
using System.Linq;

namespace Dados.Repositorio
{
    public class BonusRepositorio : IBonusRepositorio
    {
        private readonly BancoContext _bancoContext;

        public BonusRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public BonusModel ListarBonusPorId(int id) 
        {
            return _bancoContext.bonusModels.FirstOrDefault(x => x.Id == id);
        }

        public List<BonusModel> BuscarTodosBonus()
        {
            return _bancoContext.bonusModels.ToList();
        }
    }
}
