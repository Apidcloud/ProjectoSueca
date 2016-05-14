using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sueca0._1
{
    class Jogo
    {
        private List<Vaza> vazas;
        private Carta trunfo;
        private Equipa[] equipas;
        private Carta[] baralho;
        private int numVazas;

        public Jogo() { }

        public Jogo(List<Vaza> vazas, Carta trunfo, Equipa[] equipas, Carta[] baralho) {
            this.vazas = vazas;
            this.trunfo = trunfo;
            this.equipas = equipas;
            this.baralho = baralho;
        }

        public Carta Trunfo
        {
            get { return trunfo; }
            set { trunfo = value; }
        }

        public List<Vaza> Vazas
        {
            get { return vazas; }
            set { vazas = value; }
        }

        public Equipa[] Equipas
        {
            get { return equipas; }
            set { equipas = value; }
        }

        public void darCartas() { //baralha e dá as cartas 
        
        }

        public void executaJogo(){
            inicializa();
            darCartas();
            for (numVazas = 0; numVazas < 10;numVazas++)
            {
                Vaza vaza = new Vaza();
                vazas.Add(vaza);
                vaza.efetuaJogada();
                //vaza.Maior;
            }
        }

        public void inicializa() { 
        
        }

    }
}
