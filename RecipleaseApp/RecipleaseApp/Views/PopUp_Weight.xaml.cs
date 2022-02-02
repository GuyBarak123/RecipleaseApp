using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RecipleaseApp.ViewModels;

namespace RecipleaseApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUp_Weight : Popup
    {
        public PopUp_Weight()
        {


            InitializeComponent();
        }
        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Dismiss(null);
        }
    }
}