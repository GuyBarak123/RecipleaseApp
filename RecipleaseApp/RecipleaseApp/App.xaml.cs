using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RecipleaseApp.Views;
using RecipleaseApp.Models;
using System.Collections.Generic;

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
        public List<Gender> Genders { get; set; }
        
        public App()
        {
            InitializeComponent();
            Genders = new List<Gender>();
            Genders.Add(new Gender()
            {
                GenderId = 1,
                GenderName = "male"
            });
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
