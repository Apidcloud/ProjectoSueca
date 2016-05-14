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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Phone.Classes.Models;

namespace Phone.Controls
{
    public partial class HandUpRight : UserControl
    {
        public event ObjectEventHandler DoubleClick;

        private bool show;

        public HandUpRight()
        {
            InitializeComponent();
            LayoutRoot.Background = new SolidColorBrush(Colors.Transparent);
            foreach (UIElement ctrl in LayoutRoot.Children)
            {
                if (ctrl.GetType() == typeof(Image))
                {
                    Image pic = ((Image)ctrl);
                    pic.DoubleTap += pic_DoubleTap;
                }
            }
        }

        public void pic_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (DoubleClick != null)
            {
                DoubleClick(sender);
            }
        } 

        public void ChangeHandCard(int pos, string path)
        {
            foreach (UIElement ctrl in LayoutRoot.Children)
            {
                if (ctrl.GetType() == typeof(Image))
                {
                    Image pic = ((Image)ctrl);
                    if (pic.Name.ToString() == "Card" + pos.ToString())
                    {
                        if (path == "")
                        {
                            pic.Source = null;
                            pic.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            pic.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
                            pic.Visibility = Visibility.Visible;
                            
                            //pic.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                        return;
                    }
                }
            }
        }

        //public void DrawHand(Team[] oEquipas, int id, bool show = false)
        //{
        //    this.show = show;
        //    this.equipas = oEquipas;

        //    string path = "/Resources/Cards/backside.png";
            
        //    for (int i = 0; i < LayoutRoot.Children.Count; i++)
        //    {
        //        if (this.show)
        //        {
        //            path = "/Resources/Cards/" + this.equipas[1].jogadores[id].Mao[i].naipe.ToString() + "/" +
        //            this.equipas[1].jogadores[id].Mao[i].identificador.ToString() + ".png";
        //        }
        //        ((Image)LayoutRoot.Children[i]).Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
        //    }
        //}
    }
}
