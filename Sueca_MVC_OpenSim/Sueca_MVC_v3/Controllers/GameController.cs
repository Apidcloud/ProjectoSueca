using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sueca_MVC_v3.Controllers
{
    public class GameController
    {
        private Models.GameModel model = null;
        private Views.MainForm mainView = null;
        private Views.Table tableView = null;

        private View _view = null;

        public View view
        {
            set
            {
                _view = value;
                _view.UserEscolheuCarta += _view_UserEscolheuCarta;
                _view.UserIniciaJogo += _view_UserIniciaJogo;
                _view.SaveGame += _view_SaveGame;
                _view.AskToLoadSavedData += _view_AskToLoadSavedData;

            }
        }

        void _view_AskToLoadSavedData(string text)
        {
            model.InitializeSavedData(text);
        }

        void _view_SaveGame(string text)
        {
            model.SerializeToXML(text);
        }

        void _view_UserIniciaJogo(string texto)
        {
            tableView_AskToReadDeck();
            mainView_AskGameStart(texto);
        }

        void _view_UserEscolheuCarta(int posicaoCarta)
        {
            tableView_ClickedCard(posicaoCarta);
        }


        public Views.Table TableView
        {
            set
            {
                tableView = value;
                // eventos
                tableView.ClickedCard += tableView_ClickedCard;
            }
        }

        void tableView_ClickedCard(int pos)
        {
            if (model.ActivePlayer.Mao[pos] != null && model.EnableCard(pos))
            {
                model.PlayCard(model.ActivePlayer, (model.ActivePlayer as Models.HumanPlayer).Joga(pos));
                if (!model.VazaComplete())
                {
                    model.NextPlayer();
                }
                timer.Start();
            }
        }

        public Views.MainForm MainView
        {
            set
            {
                mainView = value;
                mainView.AskGameStart += mainView_AskGameStart;
                mainView.AskToInitializeData += tableView_AskToReadDeck;
                // eventos
            }
        }

      public static System.Timers.Timer timer = new System.Timers.Timer(); // usado para passar de jogador em jogador

        public GameController()
        {
            timer.Interval = 5000;
            timer.Enabled = false;
            timer.Stop();
            timer.Elapsed += timer_Tick;
        }
        

        void timer_Tick(object sender, EventArgs e)
        {
            if (model.partidas[model.curPartida].vazas.Count > 10)
            {
                model.givePoints();
                if (model.equipas[0].pontosJogo < 4 && model.equipas[1].pontosJogo < 4)
                {
                    model.createNewPartida();
                }
                else
                {
                    timer.Stop();
                    _view.ShowMessage("O Jogo terminou");
                }
            }
            else
            {
                if (model.VazaComplete())
                {
                    model.NextPlayer();

                    timer.Start();
                    return;
                }

                if (model.ActivePlayer is Models.BotPlayer)
                {
                    Models.Carta temp = model.ActivePlayer.Joga(model.partidas[model.curPartida].vazas[model.partidas[model.curPartida].vazas.Count - 1]);
                    model.PlayCard(model.ActivePlayer, temp);
                    if (model.VazaComplete())
                    {
                        return; // para visualizar a ultima jogada
                    }
                    model.NextPlayer();
                }
                else
                {
                    timer.Stop();
                }
            }
        }

        void tableView_AskToReadDeck()
        {
            // nome do ficheiro do baralho: deck.bin
            try
            {
                this.model.ReadDeck("deck.bin");
            }
            catch (Exception ex)
            {
                this.tableView.ShowMessage(ex.Message);
            }
        }

        void mainView_AskGameStart(string text)
        {
            model.GameStart(text);
        }

        public Models.GameModel Model  { set { model = value; } }

    }
}
