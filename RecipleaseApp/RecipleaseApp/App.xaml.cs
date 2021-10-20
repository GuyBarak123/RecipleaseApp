using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RecipleaseApp.Views;
using RecipleaseApp.Models;

namespace RecipleaseApp
{
    public partial class App : Application
    {
        public static bool IsDevEnv 
        { 
            get
            {
                return true;
            }   

        }

        public  User TheUser { get; set; }
        
        public App()
        {
            InitializeComponent();

            MainPage = new LogInView();
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
