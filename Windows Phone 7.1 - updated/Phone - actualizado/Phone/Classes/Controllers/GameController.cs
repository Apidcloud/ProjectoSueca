using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phone.Classes.Controllers
{
    public class GameController
    {
        private Models.GameModel model = null;
        private JogoPage mainView = null;

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

        public JogoPage MainView
        {
            set
            {
                mainView = value;
                mainView.AskGameStart += mainView_AskGameStart;
                mainView.AskToInitializeData += tableView_AskToReadDeck;
                mainView.ClickedCard += tableView_ClickedCard;
                mainView.SaveGame += mainView_SaveGame;
                mainView.AskToLoadSavedData += mainView_AskToLoadSavedData;
                // eventos
            }
        }

        void mainView_AskToLoadSavedData(string text)
        {
            model.InitializeSavedData(text);
        }

        void mainView_SaveGame(string text)
        {
            model.SerializeToXML(text);
        }

        public static System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer(); // usado para passar de jogador em jogador

        public GameController()
        {
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Stop();
            timer.Tick += timer_Tick;
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
                    //tableView.ShowMessage("A partida terminou");
                    this.mainView.GoBack();
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
                this.model.ReadDeck("deck.xml");
            }
            catch (Exception ex)
            {
                //this.tableView.ShowMessage(ex.Message);
            }
        }

        void mainView_AskGameStart(string text)
        {
            model.GameStart(text);
        }

        public Models.GameModel Model { get { return model; }  set { model = value; } }

    }
}
