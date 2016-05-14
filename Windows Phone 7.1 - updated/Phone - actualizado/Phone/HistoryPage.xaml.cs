using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Phone.Classes.Models;
using System.Windows.Media.Imaging;

namespace Phone
{
    public partial class HistoryPage : PhoneApplicationPage
    {
        private Partida partida;

        public HistoryPage()
        {
            InitializeComponent();
            this.partida = (App.Current as App).currentPartida;
            showList();
        }

        //public HistoryPage(Partida partida)
        //{
        //    InitializeComponent();
        //    this.partida = partida;
        //    showList();
        //}

        private void showList()
        {
            this.listPlays.Items.Clear();
            for (int i = 0; i < partida.vazas.Count-1; i++)
            {
                int n = i + 1;
                this.listPlays.Items.Add("Vaza nº " + n);
            }
        }

        private void listPlays_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = this.listPlays.SelectedIndex;
            if (index == -1) return;
            Vaza vaza = this.partida.vazas[index];

            foreach (Jogada jogada in vaza.jogadas)
            {
                int id = jogada.jogador.ID;
                Image pic = controlFind("picJogada" + id);
                pic.Source = new BitmapImage(new Uri("/Resources/Cards/" + jogada.carta.naipe.ToString() + "/" + jogada.carta.identificador + ".png", UriKind.RelativeOrAbsolute));
            }
        }


        Image controlFind(string key)
        {
            foreach (UIElement ctrl in ContentPanel.Children)
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

    }
}