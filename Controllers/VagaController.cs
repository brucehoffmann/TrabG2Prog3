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
                ModelState.AddModelError(string.Empty, "Vaga já cadastrada!");
                return View(model);
                //return BadRequest(new { status = "failed", message = "Vaga já cadastrada!" });
            }

            _vagaRepository.AddVaga(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("[controller]/[action]/{id}")]
        public IActionResult Ocupar(int id)
        {
            try
            {
                _vagaRepository.OcuparVaga(id);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { status = "failed", message = e.Message });
            }

        }

        [HttpPost("[controller]/[action]/{id}")]
        public IActionResult Liberar(int id)
        {
            try
            {
                _vagaRepository.LiberarVaga(id);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { status = "failed", message = e.Message });
            }
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

        [HttpDelete("[controller]/[action]/{id}")]
        public IActionResult DeletaVaga(int id)
        {
            try
            {
                _vagaRepository.DeleteVaga(id);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { status = "failed", message = e.Message });
            }
        }

    }
}