using RecipleaseApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecipleaseApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManagerGraphsView : ContentView
    {
        public ManagerGraphsView()
        {
            this.BindingContext = new ManagerGraphsViewModel();
            InitializeComponent();
        }
    }
}