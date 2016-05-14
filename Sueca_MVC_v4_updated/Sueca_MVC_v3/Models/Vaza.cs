using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sueca_MVC_v3.Models
{
    public class Vaza
    {
        public List<Jogada> jogadas { get; private set; }
        private int pontosVaza;

        private int contaJogadas;

        //public Jogada maior { get; set; }

        //public int VezdeJogar { get; set; }


        // Construtor por defeito
        public Vaza()
        {
            this.jogadas = new List<Jogada>();
            this.pontosVaza = 0;
            this.contaJogadas = 0;
        }

        // construtor por parâmetros
        public Vaza(List<Jogada> jogadas)
        {
            this.jogadas = jogadas;
            this.pontosVaza = 0;
        }

        

        //public void guardaJogada(Jogada jogada)
        //{
        //    if (contaJogadas < 4)
        //    {
        //        this.jogadas.Add(jogada);
        //        this.pontosVaza += jogada.carta.pontos;
        //        if (jogada.carta.rank > this.maior.carta.rank && contaJogadas > 1)
        //        {
        //            this.maior = jogada;
        //        }
        //    }
        //}

        public NAIPE NaipeVaza()
        {
            if (this.jogadas.Count() > 0)
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
