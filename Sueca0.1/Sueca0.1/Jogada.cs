using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sueca0._1
{
    class Jogada
    {
        public Jogador jogador { get; set; }

        public Carta carta { get; set; }


        public Jogada() { }

        public Jogada(Jogador jogador, Carta carta) {
            this.carta = carta;
            this.jogador = jogador;
        }

    }
}
