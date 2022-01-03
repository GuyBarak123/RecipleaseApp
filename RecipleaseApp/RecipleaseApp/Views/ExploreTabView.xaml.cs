using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipleaseApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecipleaseApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExploreTabView : ContentView
    {
        public ExploreTabView()
        {
            ExploreViewModel context = new ExploreViewModel();
            context.NavigateToPageEvent += (p) => Navigation.PushAsync(p);

            this.BindingContext = context;
            InitializeComponent();
        }
    }
}