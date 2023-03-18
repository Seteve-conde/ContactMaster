using Dominio.Models;
using System.Collections.Generic;

namespace Dominio.Interfaces
{
    public interface IBonusRepositorio
    {
        BonusModel ListarBonusPorId(int id);
        List<BonusModel> BuscarTodosBonus();
        
            
        
    }
}
