using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaDeVagas.Models;
using SistemaDeVagas.Repository;

namespace SistemaDeVagas.Controllers
{
    public class VagaController : Controller
    {
        private readonly VagaRepository _vagaRepository;

        public VagaController(VagaRepository vagaRepository)
        {
            _vagaRepository = vagaRepository;
        }
        public IActionResult Index()
        {
            _vagaRepository.CreateInitial();
            return View();
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Adicionar(VagaModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (!_vagaRepository.ValidaVaga(model))
            {
                return BadRequest(new { status = "failed", message = "Vaga já cadastrada!" });
                //return RedirectToAction(nameof(Adicionar));
            }
            
            _vagaRepository.AddVaga(model);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult GetVagas()
        {
            var vagas = _vagaRepository.GetVagas();
            return Json(new { data = vagas, draw = 0, recordsTotal = vagas.Count(), recordsFiltered = vagas.Count() });
        }
    }
}
