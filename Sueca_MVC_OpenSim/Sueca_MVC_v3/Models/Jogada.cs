using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sueca_MVC_v3.Models
{
    public class Jogada
    {
        public IPlayer jogador { get; private set; }

        public Carta carta { get; private set; }


        public Jogada() { }

        public Jogada(IPlayer jogador, Carta carta)
        {
            this.carta = carta;
            this.jogador = jogador;
        }

    }
}
