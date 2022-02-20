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
          
            this.BindingContext = context;
            context.SetImageSourceEvent += OnSetImageSource;
            InitializeComponent();
        }

        public void OnSetImageSource(ImageSource imgSource)
        {
            theImage.Source = imgSource;
        }
    }
}