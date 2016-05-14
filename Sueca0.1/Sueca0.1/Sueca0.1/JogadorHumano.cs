using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sueca0._1
{
    public class JogadorHumano: Jogador
    {

        public JogadorHumano() {  }

        public JogadorHumano(List<Carta> mao, string nome): base(mao, nome) { }

        public override Carta jogar(Vaza vaza)
        {
            throw new NotImplementedException();
        }

    }
}
