using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using RecipleaseApp.Services;

namespace RecipleaseApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            RecipleaseAPIProxy proxy = RecipleaseAPIProxy.CreateProxy();
            lbl.Text = await proxy.TestAsync();
        }
    }
}
