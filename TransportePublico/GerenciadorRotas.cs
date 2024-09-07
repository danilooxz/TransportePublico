using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportePublico
{
    public class GerenciadorRotas
    {
        public List<Rota> Rotas { get; private set; }

        public GerenciadorRotas()
        {
            Rotas = new List<Rota>();
        }

        public void AdicionarRota(int numero, string nome)
        {
            if (Rotas.Any(r => r.Numero == numero))
            {
                throw new Exception("Rota já existe.");
            }
            Rotas.Add(new Rota(numero, nome));
        }

        public void RemoverRota(int numero)
        {
            var rota = BuscarRota(numero);
            if (rota != null)
            {
                Rotas.Remove(rota);
            }
            else
            {
                throw new Exception("Rota não encontrada.");
            }
        }

        public Rota BuscarRota(int numero)
        {
            return Rotas.FirstOrDefault(r => r.Numero == numero);
        }

        public List<Rota> ListarRotas()
        {
            return Rotas;
        }

        public bool VerificarConflitos()
        {
            foreach (var rota in Rotas)
            {
                foreach (var parada in rota.Paradas)
                {
                    var conflito = Rotas.Any(r => r.Paradas.Any(p => p.Nome == parada.Nome &&
                        (p.HorarioChegada < parada.HorarioSaida && p.HorarioSaida > parada.HorarioChegada)));
                    if (conflito)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
