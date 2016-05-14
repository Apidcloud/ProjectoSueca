using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sueca0._1
{
    
    public enum NAIPE { ouros, copas, espadas, paus, nenhum };
    [Serializable]
    public class Carta
    {
        public int rank { get; set; }

        public NAIPE naipe { get; set; }

        public int pontos { get; set; }
        public int identificador { get; set; }

        public Carta(NAIPE naipe, int pontos, int identificador, int rank)
        {
            this.identificador = identificador;
            this.pontos = pontos;
            this.naipe = naipe;
            this.rank = rank;
        }
    }

}
