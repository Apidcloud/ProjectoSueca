using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sueca0._1
{
    class Equipa
    {
        private Jogador[] jogadores;

        public int pontosJogo { get; set; }
        public int pontosPartida { get; set; }

        public Equipa() 
        {
            this.jogadores = new Jogador[2];
            this.pontosJogo = 0;
            this.pontosPartida = 0;
        }

        public Equipa(Jogador[] jogadores) {
            this.jogadores = jogadores;
            this.pontosJogo = 0;
            this.pontosPartida = 0;
        }

    }
}
