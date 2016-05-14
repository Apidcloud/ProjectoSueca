
namespace Phone.Classes.Models
{
    public class Jogada
    {
        public IPlayer jogador { get; private set; }

        public Carta carta { get; private set; }


        public Jogada() { }

        public Jogada(IPlayer jogador, Carta carta)
        {
            this.carta = carta;
            this.jogador = jogador;
        }

    }
}
