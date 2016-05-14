using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sueca_MVC_v3.Models
{
    public interface IPlayer
    {
        event IPlayerEventHandler RefreshHand;

        List<Carta> Mao { get; }
        string Nome { get; set; }
        int tempPosicao { get; set; }

        int ID { get; set; }

        void Receber(List<Carta> cartas);
        void Retirar(int pos);
        Carta Joga(Vaza vaza);
        int ContaCartas();
        void ReceberComNulas(List<Carta> cartas);
    }
}
