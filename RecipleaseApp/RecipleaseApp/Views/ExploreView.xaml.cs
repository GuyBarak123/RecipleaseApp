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
    public partial class ExploreView : ContentPage
    {
        public ExploreView()
        {
            ExploreViewModel vm = new ExploreViewModel();
            this.BindingContext = vm;
            InitializeComponent();
        }
    }
}