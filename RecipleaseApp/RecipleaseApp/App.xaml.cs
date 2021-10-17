using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RecipleaseApp.Views;

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
