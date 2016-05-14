using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sueca_MVC_v3.Models
{
    public class HumanPlayer : IPlayer
    {

        #region Events
        public event IPlayerEventHandler RefreshHand;
        #endregion

        #region Fields
        List<Carta> mao = new List<Carta>();
        string nome;
        int id;
        #endregion

        List<Carta> IPlayer.Mao
        {
            get { return mao; }
        }

        public string Nome
        {
            get
            {
                return nome;
            }
            set
            {
                nome = value;
            }
        }

        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public void Receber(List<Carta> cartas)
        {
            if (cartas != null)
            {
                this.mao.AddRange(cartas);
                this.mao.Sort((x, y) => x.identificador - y.identificador);
                //this.mao.OrderByDescending(x => x.identificador).ToList();
                if (RefreshHand != null)
                {
                    RefreshHand(this);
                }
            }
        }

        public void Retirar(int pos)
        {
            if (pos >= 0 && pos <= 9)
            {
                this.mao[pos] = null;
                if (RefreshHand != null)
                {
                    RefreshHand(this);
                }
            }
        }

        public int tempPosicao { get; set; }

        public Carta Joga(int posicao)
        {
            Carta carta = mao[posicao]; // escolhe a carta e...
            tempPosicao = posicao;
            return carta;
        }

        public Carta Joga(Vaza vaza)
        {
            throw new NotImplementedException();
        }
        public int ContaCartas()
        {
            int r = 0;
            foreach (Carta carta in mao)
            {
                if (carta != null) r++;
            }
            return r;
        }
         public void ReceberComNulas(List<Carta> cartas)
        {
            if (cartas != null)
            {
                this.mao.AddRange(cartas);
                if (RefreshHand != null)
                {
                    RefreshHand(this);
                }
            }
        }
    }
}
