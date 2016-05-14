using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Phone.Classes;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Phone.Classes.Models;
using Phone.Resources;
using Phone.Controls;

namespace Phone
{
    public partial class JogoPage : PhoneApplicationPage
    {

        private bool DEBUG = true;

        #region Events
        public event StringEventHandler AskGameStart;
        public event EmptyEventHandler AskToInitializeData;
        public event IntEventHandler ClickedCard;
        public event StringEventHandler SaveGame;
        public event StringEventHandler AskToLoadSavedData;
        #endregion

        private GameModel model = null;

        public GameModel Model
        {
            set
            {
                model = value;
                model.AnswerInitializeData += model_AnswerInitializeData;
                model.VazaChanged += model_VazaChanged;
                model.CardsSet += model_CardsSet;
                model.EnableActivePlayer += model_EnableActivePlayer;
                model.ChangeGamePoints += model_ChangeGamePoints;
                model.ChangePoints += model_ChangePoints;
                // respostas
                // eventos
            }
        }

        void model_ChangePoints(int a, int b)
        {
            if (a == 0)
            {
                TextBlock block = this.controlFindBlock("partidaEquipaA", GridPontuacao.Children);
                block.Text = b.ToString();
            }
            else
            {
                TextBlock block = this.controlFindBlock("partidaEquipaB", GridPontuacao.Children);
                block.Text = b.ToString();
            }
                
        }

        void model_ChangeGamePoints(int a, int b)
        {
            if (a == 0)
            {
                TextBlock block = this.controlFindBlock("jogoEquipaA", GridPontuacao.Children);
                block.Text = b.ToString();
            }
            else
            {
                TextBlock block = this.controlFindBlock("jogoEquipaB", GridPontuacao.Children);
                block.Text = b.ToString();
            }
        }

        public JogoPage()
        {
            InitializeComponent();
            App.controller.MainView = this;
            this.Model = App.controller.Model;
            this.handUsuario.DoubleClick += handUsuario_DoubleClick;

            #region Application Bar Menu
            ApplicationBar = new ApplicationBar();

            ApplicationBar.Mode = ApplicationBarMode.Default;
            ApplicationBar.Opacity = 1.0;
            ApplicationBar.IsVisible = false;
            ApplicationBar.IsMenuEnabled = false;

            ApplicationBarIconButton buttonPontuacao = new ApplicationBarIconButton();
            buttonPontuacao.IconUri = new Uri("/icons_dark/appbar.upload.rest.png", UriKind.Relative);
            buttonPontuacao.Click += buttonPontuacao_Click;
            buttonPontuacao.Text = "Pontuation";

            ApplicationBarIconButton buttonHistorico = new ApplicationBarIconButton();
            buttonHistorico.IconUri = new Uri("/icons_dark/appbar.edit.rest.png", UriKind.Relative);
            buttonHistorico.Click += buttonHistorico_Click;
            buttonHistorico.Text = "History";

            ApplicationBarIconButton buttonExit = new ApplicationBarIconButton();
            buttonExit.IconUri = new Uri("/icons_dark/appbar.stop.rest.png", UriKind.Relative);
            buttonExit.Click += buttonExit_Click;
            buttonExit.Text = "Exit";

            ApplicationBar.Buttons.Add(buttonPontuacao);
            ApplicationBar.Buttons.Add(buttonHistorico);
            ApplicationBar.Buttons.Add(buttonExit);
            #endregion

            GridAvatars.Visibility = Visibility.Collapsed;
            GridGame.Visibility = Visibility.Collapsed;
            GridClose.Visibility = Visibility.Collapsed;
            GridPontuacao.Visibility = Visibility.Collapsed;
            GridGame.Visibility = Visibility.Collapsed;
            GridFill.Visibility = Visibility.Visible;

        }

        void buttonHistorico_Click(object sender, EventArgs e)
        {
            (App.Current as App).currentPartida = this.model.partidas[this.model.curPartida];
            NavigationService.Navigate(new Uri("/HistoryPage.xaml", UriKind.Relative));
        }


        


        void model_VazaChanged()
        {

            if (model.partidas[model.curPartida].vazas[model.partidas[model.curPartida].vazas.Count - 1].getNumeroJogadas() == 0)
            {
                foreach (UIElement ctrl in GridGame.Children)
                {
                    if (ctrl.GetType() == typeof(Image))
                    {
                        Image pic = ((Image)ctrl);
                        if (pic.Name.Contains("picJogada"))
                        {
                            pic.Source = null;
                        }
                    }
                }
            }
            else
            {
                foreach (Jogada jogada in model.partidas[model.curPartida].vazas[model.partidas[model.curPartida].vazas.Count - 1].jogadas)
                {
                    int id = jogada.jogador.ID;

                    Image pic = controlFind("picJogada" + id);
                    //Image pic = this.panelVaza.Controls.Find("picJogada" + id, true)[0] as PictureBox;
                    pic.Source = new BitmapImage(new Uri("/Resources/Cards/" + jogada.carta.naipe.ToString() + "/" + jogada.carta.identificador + ".png", UriKind.RelativeOrAbsolute));
                }
            }
        }

        void model_CardsSet()
        {
            // trunfo
            this.picTrunfo.Source = new BitmapImage(new Uri("/Resources/Cards/" +  model.partidas[model.curPartida].trunfo.naipe.ToString() + "/" + model.partidas[model.curPartida].trunfo.identificador + ".png", UriKind.RelativeOrAbsolute));
            
            for (int i = 0; i < 4; i++)
            {
                TextBlock playername = this.controlFindBlock("lblPlayerName" + i, GridGame.Children);
                playername.Text = model.equipas[i / 2].jogadores[i % 2].Nome;
            }
        }

        void model_EnableActivePlayer()
        {
            int id = model.ActivePlayer.ID;
            for (int i = 0; i < 4; i++)
            {
                TextBlock playername = controlFindBlock("lblPlayerName" + i, GridGame.Children);
                playername.Foreground = id == i ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.White);
            }
            this.handUsuario.IsEnabled = model.ActivePlayer is HumanPlayer;
        }

        Image controlFind(string key)
        {
            foreach (UIElement ctrl in GridGame.Children)
            {
                if (ctrl.GetType() == typeof(Image))
                {
                    Image pic = ((Image)ctrl);
                    if (pic.Name == key)
                    {
                        return pic;
                    }
                }
            }
            return null;
        }


        TextBlock controlFindBlock(string key, UIElementCollection collection)
        {
            foreach (UIElement ctrl in collection)
            {
                if (ctrl.GetType() == typeof(TextBlock))
                {
                    TextBlock pic = ((TextBlock)ctrl);
                    if (pic.Name == key)
                    {
                        return pic;
                    }
                }
            }
            return null;
        }
	

        void model_AnswerInitializeData()
        {
            for (int i = 0; i < 4; i++)
            {
                IPlayer player = model.partidas[model.curPartida].Equipas[i % 2].jogadores[i / 2];
                player.RefreshHand += player_RefreshHand;
            }
        }

        void player_RefreshHand(IPlayer player)
        {
            switch (player.ID)
            {
                // jogador
                case 0:
                    DrawHands(this.handUsuario, player);
                    break;
                // parceiro
                case 1:
                    DrawHands(this.handParceiro, player, true);
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

        private void DrawHands(UserControl hand, IPlayer player, bool backside = false)
        {
            string pathTest;
            for (int i = 0; i < 10; i++)
            {
                if (!backside || DEBUG)
                {
                    if (player.Mao[i] != null)
                    {
                        pathTest = "/Resources/Cards/" + player.Mao[i].naipe.ToString() + "/" + player.Mao[i].identificador + ".png";
                    }
                    else
                        pathTest = "";

                }
                else
                    if (player.Mao[i] != null)
                        pathTest = "/Resources/Cards/backside.png";
                    else
                        pathTest = "";


                if (hand is HandHorizontal)
                {
                    ((HandHorizontal)hand).ChangeHandCard(i, pathTest);
                }
                else if (hand is HandUpRight)
                {
                    ((HandUpRight)hand).ChangeHandCard(i, pathTest);
                }


            }
        }

        

        void handUsuario_DoubleClick(object o)
        {
            Image p = o as Image;
            int pos = Convert.ToInt32(p.Name.Substring(4));
            if (ClickedCard != null)
            {
                ClickedCard(pos);
            }
        }


       

        //private void DrawAvatars()
        //{
        //    BitmapImage src = new BitmapImage(new Uri("/Resources/Avatars/0.png", UriKind.RelativeOrAbsolute));
        //    avatarUsuario.Source = src;
        //    BitmapImage src1 = new BitmapImage(new Uri("/Resources/Avatars/1.png", UriKind.RelativeOrAbsolute));
        //    avatarBotEsquerda.Source = src1;
        //    BitmapImage src2 = new BitmapImage(new Uri("/Resources/Avatars/2.png", UriKind.RelativeOrAbsolute));
        //    avatarParceiro.Source = src2;
        //    BitmapImage src3 = new BitmapImage(new Uri("/Resources/Avatars/3.png", UriKind.RelativeOrAbsolute));
        //    avatarBotDireita.Source = src3;
        //    //pic.Image = ImageUtilities.ResizeImage(src, newWidth, newHeight);
        //    //pic.Image = ImageUtilities.ChangeOpacity(pic.Image, 0.5f);
        //}



        private void close()
        {
            if (!canClose && this.GridGame.Visibility == Visibility.Visible)
            {
                // para não haver sobreposição de grids
                this.GridPontuacao.Visibility = Visibility.Collapsed;
                this.ApplicationBar.IsMenuEnabled = false;
                //this.GridGame.Visibility = Visibility.Collapsed;

                this.GridClose.Visibility = Visibility.Visible;
            }
        }

       
        void buttonPontuacao_Click(object sender, EventArgs e)
        {
            switch (GridPontuacao.Visibility)
            {
                case Visibility.Collapsed:
                    GridPontuacao.Visibility = Visibility.Visible;
                    break;
                case Visibility.Visible:
                    GridPontuacao.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private bool goback;
        void buttonExit_Click(object sender, EventArgs e)
        {
            goback = !canClose;
            close();
        }

        private bool canClose = false;
        private void PhoneApplicationPage_BackKeyPress_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !canClose;
           // model.
            close();
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            canClose = true;
            goback = true;
            this.GridClose.Visibility = Visibility.Collapsed;
            if (SaveGame != null)
            {
                SaveGame("savedData.xml");  //MyFolder\\
            }
            NavigationService.GoBack();
            // codigo para gravar
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            canClose = true;
            goback = true;
            this.GridClose.Visibility = Visibility.Collapsed;
            NavigationService.GoBack();
            // aqui nao grava
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            canClose = false;
            goback = false;
            this.GridClose.Visibility = Visibility.Collapsed;
            this.ApplicationBar.IsMenuEnabled = true;
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtName.Text.Trim() != "")
            {
                btnConfirm.IsEnabled = true;
            }
            else
                btnConfirm.IsEnabled = false;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (AskGameStart != null)
            {
                AskGameStart(txtName.Text);
                GridFill.Visibility = Visibility.Collapsed;
                GridGame.Visibility = Visibility.Visible;
                GridAvatars.Visibility = Visibility.Visible;
                ApplicationBar.IsVisible = true;
                ApplicationBar.IsMenuEnabled = true;
            }

        }

        private void PhoneApplicationPage_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (AskToInitializeData != null && (App.Current as App).load == "false")
            {
                AskToInitializeData();
            }
            else if (AskToLoadSavedData != null && (App.Current as App).load == "true")
            {
                AskToLoadSavedData("saveData.xml"); //MyFolder
            }
        }


        public void GoBack()
        {
            NavigationService.GoBack();            
        }
    }
}