using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sueca0._1
{
    class Partida
    {
        public Carta trunfo{ get; set; }
        public List<Vaza> vazas{ get; set; }
        public int numVazas { get; set; }
        public List<Carta> baralho { get; set; }

        public Partida()
        {
            this.vazas = new List<Vaza>();
            this.trunfo = new Carta(NAIPE.nenhum, -1, -1, -1);
            this.baralho = new List<Carta>();
        }


        public Partida(List<Carta> baralho)
        {
            this.baralho = new List<Carta>();
            this.vazas = new List<Vaza>();
            this.trunfo = new Carta(NAIPE.nenhum, -1, -1, -1);
            copiaBaralho(baralho);
        }

        private void copiaBaralho(List<Carta> baralho)
        {
            foreach (Carta carta in baralho)
            {
                Carta aux = carta;
                this.baralho.Add(aux);
            }
        }


        public void darCartas() { //baralha e dá as cartas 
            
        }

        public void executaPartida(){
            darCartas();
            for (numVazas = 0; numVazas < 10;numVazas++)
            {
                //Vaza vaza = new Vaza();
                //vazas.Add(vaza);
                //vaza.efetuaJogada();
                //vaza.Maior;
            } 
        }

        

    }
}
