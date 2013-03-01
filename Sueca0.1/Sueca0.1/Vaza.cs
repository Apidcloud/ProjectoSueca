using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sueca0._1
{
    public class Vaza
    {
        private List<Jogada> jogadas;
        private int pontosVaza;

        private int contaJogadas; 

        public Jogada maior { get; set; }


        // Construtor por defeito
        public Vaza() {
            this.jogadas = new List<Jogada>();
            this.pontosVaza = 0;
           
        }

        // construtor por parâmetros
        public Vaza(List<Jogada> jogadas) {
            this.jogadas = jogadas;
            this.pontosVaza = 0;
        }

        public void guardaJogada(Jogada jogada)
        {
            if (contaJogadas < 4)
            {
                this.jogadas.Add(jogada);
                this.pontosVaza += jogada.carta.pontos;
                if (jogada.carta.rank > this.maior.carta.rank && contaJogadas > 1)
                {
                    this.maior = jogada;
                }
            }
        }

        public NAIPE NaipeVaza()
        {
            if(this.jogadas.Count() > 0 )
            {
                return this.jogadas[0].carta.naipe;
            }
            return NAIPE.nenhum;
        }

        public int getPontos()
        {
            return this.pontosVaza;
        }

        public int getNumeroJogadas()
        {
            return this.jogadas.Count();
        }

        
    }
}
