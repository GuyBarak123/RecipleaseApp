using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RecipleaseApp.ViewModels;
using RecipleaseApp.Models;
namespace RecipleaseApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTabs : ContentPage
    {
        public void RefreshPosts()
        {
            ExploreViewModel vm = (ExploreViewModel)this.ExploreTab.BindingContext;
            vm.OnRefresh();

            ProfileViewModel vmProfile = (ProfileViewModel)this.ProfileTab.BindingContext;
            vmProfile.OnRefresh();

            //ManagerGraphsViewModel vmManager = (ManagerGraphsViewModel)this.ManagerTab.BindingContext;
            //vmManager.OnRefresh();
        }
        public MainTabs()
        {
            InitializeComponent();
            //User u = ((App)App.Current).TheUser;
           
            //    TheTabView.TabItems.Remove(ManagerTabViewItem);
            
        }

        protected override void OnAppearing()
        {
            if (ExploreTab != null && ExploreTab.BindingContext != null)
            {
                ExploreViewModel vm = (ExploreViewModel)ExploreTab.BindingContext;
                vm.InitRecipes();
            }
            if (ProfileTab != null && ProfileTab.BindingContext != null)
            {
                ProfileViewModel vm = (ProfileViewModel)ProfileTab.BindingContext;
                vm.InitRecipes();
            }
            base.OnAppearing();
        }
    }
}