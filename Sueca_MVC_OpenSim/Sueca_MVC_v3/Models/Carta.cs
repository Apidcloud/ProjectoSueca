using System;
using System.Collections;

namespace Sueca_MVC_v3.Models
{
    public enum NAIPE { ouros, copas, espadas, paus, nenhum };
    [Serializable]
    public class Carta
    {
        public int rank { get; set; }

        public NAIPE naipe { get; set; }

        public int pontos { get; set; }
        public int identificador { get; set; }

        public Carta()
        {
            this.naipe = NAIPE.nenhum;
            this.identificador = -1;
            this.rank = -1;
            this.pontos = -1;
        }
        public Carta(NAIPE naipe, int pontos, int identificador, int rank)
        {
            this.identificador = identificador;
            this.pontos = pontos;
            this.naipe = naipe;
            this.rank = rank;
        }

        
    }
}
