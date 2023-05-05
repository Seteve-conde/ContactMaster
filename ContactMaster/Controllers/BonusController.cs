using ContactMaster.Filters;
using Dominio.Interfaces;
using Dominio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactMaster.Controllers
{
    [FiltroParaUsuariosLogados]
    public class BonusController : Controller
    {
        private readonly IBonusRepositorio _bonusRepositorio;

        public BonusController(IBonusRepositorio bonusRepositorio)
        {
            _bonusRepositorio = bonusRepositorio;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IndexBonus()
        {
            List<BonusModel> bonus = _bonusRepositorio.BuscarTodosBonus();
            return View(bonus);
        }

        [HttpPost]
        public IActionResult IndexBonus(BonusModel bonus)
        {
            _bonusRepositorio.AdicionarBonus(bonus);
            return RedirectToAction(nameof(IndexBonus));
        }
    }
}
