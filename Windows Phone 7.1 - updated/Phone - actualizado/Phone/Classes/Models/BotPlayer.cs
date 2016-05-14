using System.Collections.Generic;
using System.Linq;

namespace Phone.Classes.Models
{
    public class BotPlayer : IPlayer
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

        void IPlayer.Receber(List<Carta> cartas)
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

        public Carta Joga(Vaza vaza)
        {
            for (int i = 0; i < mao.Count; i++)
            {
                if (mao[i] != null && mao[i].naipe == vaza.NaipeVaza())
                {
                    tempPosicao = i;
                    return mao[i];
                }
            }

            if (HasCards())
            {
                for (int i = 0; i < mao.Count; i++)
                {
                    if (mao[i] != null)
                    {
                        tempPosicao = i;
                        return mao[i];
                    }
                }
            }
            return null;
        }

        private bool HasCards()
        {
            return mao.Count(x => x != null) > 0;
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
