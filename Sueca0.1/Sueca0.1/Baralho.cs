using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sueca0._1
{
    [Serializable]
    public class Baralho
    {
        private List<Carta> baralho;

        public Baralho(int size = 0)
        {
            this.baralho = new List<Carta>(size);
        }

        public Carta this[int index]
        {
            get
            {
                return this.baralho[index];
            }
            set
            {
                this.baralho[index] = value;
            }
        }

        public void Add(Carta newCarta)
        {
            this.baralho.Add(newCarta);
        }

        public void Insert(int index, Carta newCarta)
        {
            this.baralho.Insert(index, newCarta);
        }

        public bool Remove(Carta newCarta)
        {
            return this.baralho.Remove(newCarta);
        }

        public int Count()
        {
            return this.baralho.Count;
        }
    }
}
