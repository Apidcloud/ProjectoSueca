using System.Collections.Generic;

namespace Phone.Classes.Models
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
        void ReceberComNulas(List<Carta> cartas);
        Carta Joga(Vaza vaza);
    }
}
