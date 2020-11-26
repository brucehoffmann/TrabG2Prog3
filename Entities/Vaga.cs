using SistemaDeVagas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeVagas.Entities
{
    public class Vaga
    {
        public int Id { get; private set; }
        public int Andar { get; private set; }
        public int Corredor { get; private set; }
        public int Numero { get; private set; }
        public string PorteVeiculo { get; private set; }
        public bool Ocupada { get; private set; }
        public Vaga(int id, bool ocupada, VagaModel model)
        {
            Id = id;
            Ocupada = ocupada;
            Andar = model.Andar;
            Corredor = model.Corredor;
            Numero = model.Numero;
            PorteVeiculo = model.PorteVeiculo;
        }


        public void OcuparVaga()
        {
            Ocupada = true;
        }

        public void LiberarVaga()
        {
            Ocupada = false;
        }
    }
}
