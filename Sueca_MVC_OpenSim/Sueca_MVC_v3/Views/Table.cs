using Sueca_MVC_v3.Models;
using Sueca_MVC_v3.Views.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Sueca_MVC_v3.Views
{
    public enum DrawPosition { BOT, TOP, LEFT, RIGHT };

    public partial class Table : Form
    {
        public static bool DEBUG = false;

        private Models.GameModel model = null;

        public Models.GameModel Model
        {
            set
            {
                model = value;
                model.AnswerInitializeData += model_AnswerInitializeData;
                model.VazaChanged += model_VazaChanged;
                model.CardsSet += model_CardsSet;
                model.EnableActivePlayer += model_EnableActivePlayer;
                model.ChangePoints += model_ChangePoints;
                model.ChangeGamePoints += model_ChangeGamePoints;
                // respostas
                // eventos
            }
        }

        void model_ChangeGamePoints(int a, int b)
        {
            if (a == 0)
                this.lblTotalA.Text = b.ToString();
            else
                this.lblTotalB.Text = b.ToString();
        }

        void model_ChangePoints(int a, int b)
        {
            if (a == 0)
                this.lblPartidaA.Text = b.ToString();
            else
                this.lblPartidaB.Text = b.ToString();
        }



        void model_EnableActivePlayer()
        {
            int id = model.ActivePlayer.ID;
            for (int i = 0; i < 4; i++)
            {
                Label lblName = this.Controls.Find("lblPlayerName" + i, true)[0] as Label;
                lblName.ForeColor = id == i ? Color.Red : Color.Black;
            }
            this.handUser.Enabled = model.ActivePlayer is Models.HumanPlayer;
        }

        void model_CardsSet()
        {
            // trunfo
            this.picTrunfo.SizeMode = PictureBoxSizeMode.StretchImage;
            this.picTrunfo.Image = Image.FromFile("Cartas\\" + model.partidas[this.model.curPartida].trunfo.naipe.ToString() + "\\" + model.partidas[this.model.curPartida].trunfo.identificador + ".png"); 
            for (int i = 0; i < 4; i++)
			{
                Label playerName = this.Controls.Find("lblPlayerName" + i, true)[0] as Label;
                playerName.Text = model.equipas[i / 2].jogadores[i % 2].Nome;
			}
        }

        #region Events
        public event IntEventHandler ClickedCard;
        
        #endregion

        public Table()
        {
            InitializeComponent();
            this.handUser.DoubleClick += handUser_DoubleClick;
        }

        void handUser_DoubleClick(object o)
        {
            PictureBox p = o as PictureBox;
            int pos = Convert.ToInt32(p.Name.Substring(3)) - 1;
            if (ClickedCard != null)
            {
                ClickedCard(pos);
            }
        }



        void model_VazaChanged()
        {
            
            if (model.partidas[this.model.curPartida].vazas[model.partidas[this.model.curPartida].vazas.Count - 1].getNumeroJogadas() == 0)
            {
                foreach (PictureBox pic in this.panelVaza.Controls)
                    pic.Image = null;
            }
            else
            {
                foreach (Models.Jogada jogada in model.partidas[this.model.curPartida].vazas[model.partidas[this.model.curPartida].vazas.Count - 1].jogadas)
                {
                    int id = jogada.jogador.ID;
                    PictureBox pic = this.panelVaza.Controls.Find("picJogada" + id, true)[0] as PictureBox;
                    pic.Image = Image.FromFile("Cartas\\" + jogada.carta.naipe.ToString() + "\\" + jogada.carta.identificador + ".png");

                }
            }
        }

        void model_AnswerInitializeData()
        {
            for (int i = 0; i < 4; i++)
            {
                Models.IPlayer player = model.partidas[this.model.curPartida].Equipas[i % 2].jogadores[i / 2];
                player.RefreshHand += player_RefreshHand;
            }
        }

        void player_RefreshHand(Models.IPlayer player)
        {
            switch (player.ID)
            {
                // jogador
                case 0:
                    DrawHands(this.handUser, player);
                    break;
                // parceiro
                case 1:
                    DrawHands(this.HandParceiro, player, true);
                    break;
                // bot direita
                case 2:
                    DrawHands(this.handBotDireita, player, true);
                    break;
                // bot esquerda
                case 3:
                    DrawHands(this.handBotEsquerda, player, true);
                    break;
                default:
                    break;
            }
        }

        private void DrawHands(UserControl hand, Models.IPlayer player, bool backside = false)
        {
            string pathTest;
            for (int i = 0; i < 10; i++)
            {
                if (!backside || Table.DEBUG)
                {
                    if (player.Mao[i] != null)
                    {
                        pathTest = "Cartas\\" + player.Mao[i].naipe.ToString() + "\\" + player.Mao[i].identificador + ".png";
                    }
                    else
                        pathTest = "";

                }
                else
                    if (player.Mao[i] != null)
                        pathTest = "Cartas\\backside.png";
                    else
                        pathTest = "";
                    

                if (hand is PlayerHand)
                {
                    ((PlayerHand)hand).ChangeHandCard(i, pathTest);
                }
                else if(hand is PlayerHandUpRight)
                {
                    ((PlayerHandUpRight)hand).ChangeHandCard(i, pathTest);
                }

                
            }
        }

        private bool shutdown = false;
        public void ShowMessage(string msg)
        {
            MessageBox.Show(msg);
            if (msg.Contains("terminou"))
            {
                shutdown = true;
                this.Close();
            }
        }

        private bool canClose = false;
        private void Table_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !canClose;
            if (!canClose && !shutdown)
            {
                SaveForm gj = new SaveForm();
                gj.ShowDialog();
                switch (gj.DialogResult)
                {
                    case DialogResult.Cancel:
                        gj.Close();
                        break;
                    case DialogResult.Yes:
                    // gravar jogo utilizando serialização XML
                    case DialogResult.No:
                        // fechar
                        canClose = true;
                        // mostrar janela principal
                        this.Close();
                        Application.Restart();
                        break;
                    default:
                        break;
                }
            }
            else if (shutdown)
	        {
                canClose = true;
                shutdown = false;
		        this.Close();
                Application.Restart();
	        }
        }

 

        private void históricoDeVazasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // abrir outra form
            // lista de vazas
            // ao selecionar uma mostrar as 4 jogadas do lado direito.
            History history = new History(model.partidas[model.curPartida]);
            history.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SerializeToXML("testSave", this.model);
            //MessageBox.Show("Saved");
        }



        public static void SerializeToXML(string filename, GameModel ob)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GameModel));
            TextWriter textWriter = new StreamWriter(filename + ".xml");
            serializer.Serialize(textWriter, ob);
            textWriter.Close();
        }

        public static Object DeserializeFromXML(string filename)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(Object));
            TextReader textReader = new StreamReader(filename);
            Object ob;
            ob = (Object)deserializer.Deserialize(textReader);
            textReader.Close();
            return ob;
        }


        
    }
}
