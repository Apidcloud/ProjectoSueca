using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sueca0._1
{
    class Equipa
    {
        private Jogador[] jogadores;//pode ser lista 
        private int pontos;

        public Equipa() { }

        public Equipa(Jogador[] jogadores, int pontos) {
            this.jogadores = jogadores;
            this.pontos = pontos;
        }

    }
}
