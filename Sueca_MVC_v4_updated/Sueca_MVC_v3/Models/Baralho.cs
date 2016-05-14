using System;
using System.Collections;
using System.Collections.Generic;

namespace Sueca_MVC_v3.Models
{
    [Serializable]
    public class Baralho : IEnumerable
    {
        private List<Carta> baralho;
        private int position = -1;


        public Baralho(int size=0)
        {
            this.baralho = new List<Carta>(size);
        }

        public Carta this[int index]
        {
            get
            {
                try
                {
                    return this.baralho[index];
                }

                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
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
        public void Add(Object fix)
        {
            this.baralho.Add(fix as Carta);
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (Carta carta in this.baralho)
            {
                yield return carta;
            }
        }


        
    }
}
