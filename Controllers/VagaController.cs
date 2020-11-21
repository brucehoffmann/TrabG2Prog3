using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaDeVagas.Entities;
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

        [HttpPut]
        public IActionResult Update(Vaga vaga)
        {
            if (!ModelState.IsValid)
                return View();

            if (vaga.Ocupada)
            {
                return BadRequest(new { status = "failed", message = "Vaga ocupada. Não pode ser editada!" });
            }

            _vagaRepository.Update(vaga);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult GetVagas()
        {
            var vagas = _vagaRepository.GetVagas();
            return Json(new { data = vagas, draw = 0, recordsTotal = vagas.Count(), recordsFiltered = vagas.Count() });
        }

        [HttpDelete]
        public IActionResult DeletaVaga(Vaga vaga)
        {
            if (vaga.Id == null)
            {
                return BadRequest(new { status = "failed", message = "Id informado está null. Vaga não pode ser deletada!" });
            }
            if (vaga.Ocupada)
            {
                return BadRequest(new { status = "failed", message = "Vaga ocupada. Não pode ser deletada!" });
            }

            _vagaRepository.DeleteVaga(vaga.Id);
            return Ok(new { status = "success", message = "Vaga deletada com sucesso" });
        }

    }
}