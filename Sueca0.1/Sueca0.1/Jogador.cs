using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sueca0._1
{
    public abstract class Jogador
    {
        private List<Carta> mao;
        private string nome;

        public Jogador() { }

        public Jogador(List<Carta> mao, string nome) {
            this.nome = nome;
            this.mao = mao;
        }

        public abstract Carta jogar(Vaza vaza);
    
    }
}
