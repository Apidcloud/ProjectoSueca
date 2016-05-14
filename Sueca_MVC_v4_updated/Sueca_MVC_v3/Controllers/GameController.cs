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

        public Views.Table TableView
        {
            set
            {
                tableView = value;
                // eventos
                tableView.ClickedCard += tableView_ClickedCard;
                tableView.SaveGame += tableView_SaveGame;
                
            }
        }

        void tableView_SaveGame(string text)
        {
            model.SerializeToXML(text);
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
                mainView.AskToLoadSavedData += mainView_AskToLoadSavedData;
                // eventos
            }
        }

        void mainView_AskToLoadSavedData(string filename)
        {
            model.InitializeSavedData(filename);
        }

        public static System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer(); // usado para passar de jogador em jogador

        public GameController()
        {
            timer.Interval = 1000;
            timer.Enabled = false;
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
                    tableView.ShowMessage("O Jogo terminou");
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
            model.InitializeData();
        }

        void mainView_AskGameStart(string text)
        {
            model.GameStart(text);
        }

        public Models.GameModel Model  { set { model = value; } }

    }
}
