using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RecipleaseApp.Models;
using RecipleaseApp.ViewModels;

namespace RecipleaseApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateRecipeView : ContentPage
    {
        Recipe currentRecipe;

        public UpdateRecipeView()
        {
        }

        public UpdateRecipeView(Recipe recp)
        {
            this.currentRecipe = recp;
            UpdateRecipeViewModel upVM = new UpdateRecipeViewModel(recp);
            this.BindingContext = upVM;
            InitializeComponent();
        }
    }
}