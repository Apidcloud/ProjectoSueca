using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sueca_MVC_v3.Models
{
    public class Team
    {
        public List<IPlayer> jogadores { get; set; }

        public int pontosJogo { get; set; }
        public int pontosPartida { get; set; }

        public Team()
        {
            this.jogadores = new List<IPlayer>(2);
            this.pontosJogo = 0;
            this.pontosPartida = 0;
        }

        public Team(List<IPlayer> jogadores)
        {
            this.jogadores = jogadores;
            this.pontosJogo = 0;
            this.pontosPartida = 0;
        }

        public void clearHand()
        {
            for (int i = 0; i < 2; i++)
            {
                this.jogadores[i].Mao.Clear();
            }
        }

        //public void SortHand()
        //{
        //    for (int i = 0; i < 2; i++)
        //    {
        //        this.jogadores[i].Mao.Sort((x, y) => x.identificador - y.identificador);
        //    }
        //}

        public void addPlayer(IPlayer player)
        {
            this.jogadores.Add(player);
        }

    }
}
