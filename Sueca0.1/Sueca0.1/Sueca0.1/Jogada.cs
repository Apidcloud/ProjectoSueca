using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sueca0._1
{
    class Jogada
    {
        private Jogador jogador;


        private Carta carta;

        internal Carta Carta
        {
            get { return carta; }
            set { carta = value; }
        }

        public Jogada() { }

        public Jogada(Jogador jogador, Carta carta) {
            this.carta = carta;
            this.jogador = jogador;
        }

        internal Jogador Jogador
        {
            get { return jogador; }
            set { jogador = value; }
        }
    }
}
