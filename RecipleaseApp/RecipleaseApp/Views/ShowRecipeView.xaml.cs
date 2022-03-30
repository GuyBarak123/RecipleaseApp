using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipleaseApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RecipleaseApp.Models;

namespace RecipleaseApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowRecipeView : ContentPage
    {
        public ShowRecipeView()
        {
           ShowRecipeViewModel context = new ShowRecipeViewModel();
            context.NavigateToPageEvent += (p) => Navigation.PushAsync(p);
            this.BindingContext = context;
            InitializeComponent();
        }
    }
}