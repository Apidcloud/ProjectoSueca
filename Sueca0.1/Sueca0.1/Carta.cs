using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sueca0._1
{
    enum NAIPE {ouros, copas, espadas, paus, nenhum};

    class Carta
    {

        public int rank { get; set; }

        public NAIPE naipe{ get; set; }

        public int pontos { get; set; }
        public int identificador { get; set; }

        //public Carta() {
        //    this.rank = 0; // ou 0 nao sei como querem
        //    this.naipe = 0;
        //    this.pontos = 0;
        //    this.identificador = -1;

        //}

        public Carta(NAIPE naipe, int pontos, int identificador, int rank) {
            this.identificador = identificador;
            this.pontos = pontos;
            this.naipe = naipe;
            this.rank = rank;
        }

        //public Carta[] criaBaralho()
        //{
        //    throw new System.NotImplementedException();
        //}

        

    }

}
