using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RecipleaseApp.Views;
using RecipleaseApp.Models;
using System.Collections.Generic;
using RecipleaseApp.Services;

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
            
          
           
            MainPage = new LogInView();

            
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
