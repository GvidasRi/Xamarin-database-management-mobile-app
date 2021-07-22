using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kursinis
{
    public partial class App : Application
    {
        public static string paskyra { get; set; }
        public static string choice { get; set; }
        public static string choice2 { get; set; }
        public static string vardas { get; set; }
        public static string vardass { get; set; }
        public static string pavarde { get; set; }
        public static int check { get; set; }
        public static int check2 { get; set; }
        public App()
        {
            InitializeComponent();

            check = 0;
            check2 = 0;
         //   MainPage = new MainPage();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
