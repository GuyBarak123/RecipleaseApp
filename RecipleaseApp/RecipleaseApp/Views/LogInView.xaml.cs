using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipleaseApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RecipleaseApp.ViewModels;

namespace RecipleaseApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInView : ContentPage
    {
        public LogInView()
        {
            LogInViewModel context = new LogInViewModel();
            this.BindingContext = context;
            InitializeComponent();
        }
        private void Password_Focused(object sender, FocusEventArgs e)
        {
            Entry entry = (Entry)sender;
            entry.IsPassword = true;
        }
    }
}