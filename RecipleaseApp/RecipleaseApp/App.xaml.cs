using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RecipleaseApp.Views;
using RecipleaseApp.Models;
using System.Collections.Generic;
using RecipleaseApp.Services;
using RecipleaseApp.Views.Trying.DashBoard;

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
        
        
        public List<Tag> Tags { get; set; }

        public LookupTables Lookups { get; set; }
        public App()
        {
            InitializeComponent();
            Lookups = new LookupTables()
            {
                Genders = new List<Gender>(),
                Tags = new List<Tag>(),
                Ingridients = new List<Ingridient>()
            };



            //
            MainPage = new NavigationPage(new LogInView())
            {
                //MainPage = new NavigationPage(new DashBoard_ContentPage());
                BarBackgroundColor = Color.FromHex("#ff5300"),
                BarTextColor = Color.White
            };
            
        }


        protected async override void OnStart()
        {
            RecipleaseAPIProxy proxy = RecipleaseAPIProxy.CreateProxy();
            this.Lookups = await proxy.GetLookupsAsync();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
