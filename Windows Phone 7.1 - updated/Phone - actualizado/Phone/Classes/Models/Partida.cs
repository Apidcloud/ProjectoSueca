using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phone.Classes.Models
{
    public class Partida
    {
        public Team[] Equipas { get; private set; }
        private int indexDar;
        public Carta trunfo { get; set; }
        public List<Vaza> vazas { get; set; }

        public int numVazas { get; set; }
        public List<Carta> baralho { get; set; }

        public Partida()
        {
            this.vazas = new List<Vaza>();
            this.trunfo = new Carta(NAIPE.nenhum, -1, -1, -1);
            this.baralho = new List<Carta>();
            this.Equipas = new Team[2];
            this.indexDar = 0;
        }


        public Partida(List<Carta> baralho, Team[] equipas, int index)
        {
            this.baralho = new List<Carta>();
            this.Equipas = equipas;
            this.clearHands();
            this.vazas = new List<Vaza>();
            this.trunfo = new Carta(NAIPE.nenhum, -1, -1, -1);
            this.indexDar = index;
            this.copiaBaralho(baralho);
            this.baralharCartas();
        }

        private void clearHands()
        {
            for (int i = 0; i < 2; i++)
            {
                this.Equipas[i].clearHand();
            }
        }

        private void copiaBaralho(List<Carta> baralho)
        {
            for (int i = 0; i < baralho.Count(); i++)
            {
                Carta aux = baralho[i];

                this.baralho.Add(aux);
            }
        }

        private void baralharCartas()
        {
            Random r = new Random();
            Carta aux;
            int auxIndex;
            for (int i = 0; i < 40; i++)
            {
                auxIndex = r.Next(i, 40);
                aux = baralho[auxIndex];
                baralho.Remove(baralho[auxIndex]);
                baralho.Insert(i, aux);
            }

            // partir baralho
            for (int i = 0; i < r.Next(0, 40); i++) // de i = 0 até carta_escolhida
            {
                Carta auxn = this.baralho[this.baralho.Count() - 1];
                this.baralho.Remove(auxn);
                this.baralho.Insert(0, auxn);
            }
        }

        public void darCartas()
        {
            int indexJogador = this.indexDar / 2;
            int indexEquipa = this.indexDar % 2;
            List<Carta> temp = new List<Carta>();

            this.trunfo = this.baralho[39];
            //  0 -> 0 a 9 ; 20 - 29.  1 -> 10 a 19; 30 a 39.
            int count = 39;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    temp.Add(this.baralho[count--]);
                }
                this.Equipas[i % 2].jogadores[i / 2].Receber(temp);
                temp.Clear();
            }

        }


    }
}
