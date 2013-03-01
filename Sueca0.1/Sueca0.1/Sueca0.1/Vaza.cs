using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sueca0._1
{
    public class Vaza
    {
        private List<Jogada> jogadas;
        private Jogada maior;

        private int contaJogadas;

        public Vaza() { }

        public Vaza(List<Jogada> jogadas) {
            this.jogadas = jogadas;
        }

        public void efetuaJogada(){
            //(...)
            
            //Jogada jogada = new Jogada(, new Carta());
            //jogada.Carta = jogada.Jogador.jogar(this);
            //(...)
        }

        public Jogada Maior
        {
            get { return maior; }
            set { maior = value; }
        }
    }
}
