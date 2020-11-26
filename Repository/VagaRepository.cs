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
            CreateInitial();
        }
        public List<Vaga> GetVagas()
        {
            return Vagas.OrderBy(v => v.Andar).ThenBy(v => v.Corredor).ThenBy(v => v.Numero).ToList();
        }

        public void AddVaga(VagaModel model)
        {
            var novaVaga = new Vaga(CriaId(), false, model);

            Vagas.Add(novaVaga);
        }

        public void Update(Vaga vaga)
        {
            Vagas.ForEach(item =>
            {
                if (item.Id == vaga.Id && !vaga.Ocupada)
                    item = vaga;
            });
        }

        public void DeleteVaga(int vagaId)
        {
            var vagaParaRemover = Vagas.FirstOrDefault(v => v.Id == vagaId);

            if (vagaParaRemover == null)
                throw new Exception("Vaga não encontrada.");

            if (vagaParaRemover.Ocupada)
                throw new Exception("Vaga está ocupada e não pode ser removida.");

            Vagas.Remove(vagaParaRemover);
        }

        public void OcuparVaga(int id)
        {
            var vaga = Vagas.FirstOrDefault(v => v.Id == id);

            if (vaga == null)
                throw new Exception("Vaga não encontrada");

            if (vaga.Ocupada)
                throw new Exception("Vaga já está ocupada");

            vaga.OcuparVaga();
        }

        public void LiberarVaga(int id)
        {
            var vaga = Vagas.FirstOrDefault(v => v.Id == id);

            if (vaga == null)
                throw new Exception("Vaga não encontrada");

            vaga.LiberarVaga();
        }

        public bool ValidaVaga(VagaModel model)
        {
            if (Vagas.Count <= 0)
                return true;

            if(Vagas.Any(v => v.Andar == model.Andar && v.Corredor == model.Corredor && v.Numero == model.Numero))
                return false;

            return true;
        }

        private int CriaId()
        {
            var novoId = 1;

            if (Vagas.Count > 0)
            {
                var ultimoId = Vagas.Select(v => v.Id).Max();
                novoId = ultimoId + 1;
            }

            return novoId;
        }

        public void CreateInitial()
        {
            var vaga1Model = new VagaModel(1, 1, 1, "Pequeno");
            var vaga2Model = new VagaModel(1, 1, 2, "Pequeno");
            var vaga3Model = new VagaModel(1, 1, 3, "Pequeno");
            var vaga4Model = new VagaModel(1, 1, 4, "Pequeno");
            var vaga5Model = new VagaModel(1, 1, 5, "Pequeno");

            Vagas.Add(new Vaga(CriaId(), false, vaga1Model));
            Vagas.Add(new Vaga(CriaId(), false, vaga2Model));
            Vagas.Add(new Vaga(CriaId(), false, vaga3Model));
            Vagas.Add(new Vaga(CriaId(), false, vaga4Model));
            Vagas.Add(new Vaga(CriaId(), false, vaga5Model));
        }
    }
}
