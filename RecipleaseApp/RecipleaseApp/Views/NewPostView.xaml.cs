using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipleaseApp.ViewModels;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecipleaseApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewPostView : ContentView
    {
        public NewPostView()
        {
            
           NewPostViewModel context = new NewPostViewModel();
            context.NavigateToPageEvent += (p) => Navigation.PushAsync(p);
            this.BindingContext = context;
            InitializeComponent();
        }
    }
}