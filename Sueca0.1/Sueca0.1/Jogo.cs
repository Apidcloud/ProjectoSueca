using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sueca0._1
{
    class Jogo
    {
        public Equipa[] equipas { get; set; }

        public List<Carta> baralho { get; set; }

        public Partida[] partidas { get; set; }

        public Jogo()
        {
            this.equipas = new Equipa[2];
            this.baralho = new List<Carta>();
            this.partidas = new Partida[4];
        }
        public void criarBaralho()
        {
            // lêr xml
            // Carta newCarta = new Carta(informação_lida do xml);
            // this.baralho.add(newCarta);
        }

        public void iniciarjogo()
        {
            // loop com as 4 partidas
            // chamar método executarPartida
        }
    }
}
