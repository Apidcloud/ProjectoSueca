using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sueca0._1
{
    enum NAIPE {ouros, copas, espadas, paus};

    class Carta
    {
        public NAIPE naipe
        {
            get;
            set;
        }
        public int pontos
        {
            get;
            set;
        }
        public int identificador
        {
            get;
            set;
        }
        public int rank
        {
            get;
            set;
        }

        public Carta() {
            this.identificador = -1;
            this.naipe = 0;
            this.rank = 0;
            this.pontos = 0;
        }

        public Carta(NAIPE naipe, int pontos, int identificador) {
            this.identificador = identificador;
            this.pontos = pontos;
            this.naipe = naipe;
        }

    }
}
