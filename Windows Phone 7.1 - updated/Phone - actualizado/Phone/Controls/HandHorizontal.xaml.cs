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

namespace Phone.Resources
{
    public partial class HandHorizontal : UserControl
    {
        public event ObjectEventHandler DoubleClick;
        private bool show;

        public HandHorizontal()
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

        void pic_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
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


        //public void DrawHand(Team[] oEquipas, bool show = true)
        //{
        //    this.show = show;
        //    this.equipas = oEquipas;

        //    int id = this.show ? 0 : 1;
        //    string path = "/Resources/Cards/backside.png";
        //    for (int i = 0; i < LayoutRoot.Children.Count; i++)
        //    {
        //        if (this.show)
        //        {
        //            path = "/Resources/Cards/" + this.equipas[0].jogadores[id].Mao[i].naipe.ToString() + "/" +
        //            this.equipas[0].jogadores[id].Mao[i].identificador.ToString() + ".png";
        //            ((Image)LayoutRoot.Children[i]).Tag = id == 0 ? 1 : 0;
        //        }
        //        ((Image)LayoutRoot.Children[i]).Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
        //        ((Image)LayoutRoot.Children[i]).Tap += HandHorizontal_Tap;
        //    }
        //}

        //public delegate void PictureHandler(string path, Image img);
        //public event PictureHandler PathSend;

        //void HandHorizontal_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        //{
        //    Image temp = ((Image)sender);
        //    if ((int)temp.Tag == 1)
        //    {
        //        if (PathSend != null)
        //        {
        //            string p = ((BitmapImage)temp.Source).UriSource.ToString();
        //            PathSend(p, temp);
        //        }
        //    }
        //}
    }
}
