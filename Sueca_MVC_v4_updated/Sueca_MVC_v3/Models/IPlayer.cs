using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sueca_MVC_v3.Models
{
    [XmlInclude(typeof(HumanPlayer))]
    [XmlInclude(typeof(BotPlayer))]
    public interface IPlayer
    {
        event IPlayerEventHandler RefreshHand;

        List<Carta> Mao { get; }
        string Nome { get; set; }
        int tempPosicao { get; set; }

        int ID { get; set; }

        void Receber(List<Carta> cartas);
        void ReceberComNulas(List<Carta> cartas);
        void Retirar(int pos);
        Carta Joga(Vaza vaza);
    }
}
