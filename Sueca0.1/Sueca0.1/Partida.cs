using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuecaLibrary;

namespace Sueca0._1
{
    public class Partida
    {
        public Carta trunfo{ get; set; }
        public List<Vaza> vazas{ get; set; }
        public int numVazas { get; set; }
        public Baralho baralho { get; set; }

        public Partida()
        {
            this.vazas = new List<Vaza>();
            this.trunfo = new Carta(NAIPE.nenhum, -1, -1, -1);
            this.baralho = new Baralho();
        }


        public Partida(Baralho baralho)
        {
            this.baralho = new Baralho();
            this.vazas = new List<Vaza>();
            this.trunfo = new Carta(NAIPE.nenhum, -1, -1, -1);
            copiaBaralho(this.baralho);
        }

        private void copiaBaralho(Baralho baralho)
        {
            for (int i = 0; i < baralho.Count(); i++)
            {
                Carta aux = baralho[i];
                this.baralho.Add(aux);
            }
        }


        public void darCartas() { //baralha e dá as cartas 
            
        }

        public void executaPartida(){
            darCartas();
            for (numVazas = 0; numVazas < 10;numVazas++)
            {
                
            } 
        }

        

    }
}
