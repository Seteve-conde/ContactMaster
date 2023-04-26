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
            return _bancoContext.BonusModels.FirstOrDefault(x => x.Id == id);
        }

        public List<BonusModel> BuscarTodosBonus()
        {
            return _bancoContext.BonusModels.ToList();
        }

        public void AdicionarBonus(BonusModel bonus)
        {
            _bancoContext.BonusModels.Add(bonus);
            _bancoContext.SaveChanges();
        }
    }
}
