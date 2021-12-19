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
    public partial class NewPostView : ContentPage
    {
        public NewPostView()
        {
            NewPostViewModel context = new NewPostViewModel();
            //Register to the event so the view model will be able to navigate to the monkeypage
            context.NavigateToPageEvent += (p) => Navigation.PushAsync(p);
            this.BindingContext = context;
            InitializeComponent();

        }
    }
}