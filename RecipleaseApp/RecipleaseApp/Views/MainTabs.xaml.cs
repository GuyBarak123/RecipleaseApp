using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RecipleaseApp.ViewModels;
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
        }
        public MainTabs()
        {
            InitializeComponent();
        }
    }
}