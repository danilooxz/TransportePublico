using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportePublico
{
    public class Parada
    {
        public string Nome { get; private set; }
        public TimeSpan HorarioSaida { get; private set; }
        public TimeSpan HorarioChegada { get; private set; }

        public Parada(string nome, TimeSpan horarioSaida, TimeSpan horarioChegada)
        {
            Nome = nome;
            HorarioSaida = horarioSaida;
            HorarioChegada = horarioChegada;
        }
    }
}