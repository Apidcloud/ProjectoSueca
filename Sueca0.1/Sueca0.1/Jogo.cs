using SuecaLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sueca0._1
{
    public class Jogo
    {

        public Equipa[] equipas { get; set; }
        public Baralho baralho { get; set; }
        public Partida[] partidas { get; set; }

        public Jogo()
        {
            this.equipas = new Equipa[2];
            this.partidas = new Partida[4];
            criarBaralho();
        }
        private void criarBaralho()
        {
            this.baralho = (Baralho)(SerializeData.DeserializeObject("deck.bin"));
        }

        public void iniciarjogo()
        {
            // loop com as 4 partidas
            // chamar método executarPartida
        }
    }
}
