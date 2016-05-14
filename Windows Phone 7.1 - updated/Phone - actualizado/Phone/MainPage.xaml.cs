using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Phone.Classes;
using Phone.Classes.Models;
//using Microsoft.Phone.Shell.ApplicationBar;

namespace Phone
{
    public partial class MainPage : PhoneApplicationPage
    {
       

        

        // Constructor
        public MainPage()
        {
            InitializeComponent();

        }

        void OnAppbarPlayClick(object sender, EventArgs args) 
        {
            NavigationService.Navigate(new Uri("/JogoPage.xaml", UriKind.Relative));
        }


        void OnAppLoadClick(object sender, EventArgs args)
        {
            (App.Current as App).load = "true";
            NavigationService.Navigate(new Uri("/JogoPage.xaml", UriKind.Relative));
        }

    }
}