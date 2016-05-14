using System.Collections.Generic;
using System.Linq;

namespace Phone.Classes.Models
{
    public class Vaza
    {
        public List<Jogada> jogadas { get; private set; }
        private int pontosVaza;

        private int contaJogadas;

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
