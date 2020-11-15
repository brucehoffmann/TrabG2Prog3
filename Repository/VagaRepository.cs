using SistemaDeVagas.Entities;
using SistemaDeVagas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeVagas.Repository
{
    public class VagaRepository
    {
        private List<Vaga> Vagas;

        public VagaRepository()
        {
            Vagas = new List<Vaga>();
        }
        public List<Vaga> GetVagas()
        {
            return Vagas.OrderBy(v => v.Andar).ThenBy(v => v.Corredor).ThenBy(v => v.Numero).ToList();
        }

        public void AddVaga(VagaModel model)
        {
            var novoId = 1;

            if (Vagas.Count > 0)
            {
                var ultimoId = Vagas.Select(v => v.Id).Max();
                novoId = ultimoId + 1;
            }

            var novaVaga = new Vaga(novoId, false, model);

            Vagas.Add(novaVaga);
        }

        public void DeleteVaga(int vagaId)
        {
            var vagaParaRemover = Vagas.FirstOrDefault(v => v.Id == vagaId);

            if (vagaParaRemover != null)
                Vagas.Remove(vagaParaRemover);
        }
    }
}
