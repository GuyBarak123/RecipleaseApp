using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipleaseApp.ViewModels;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RecipleaseApp.Models;
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
            context.RecipeUpdatedEvent += OnPostSaved;
            InitializeComponent();
        }

        private Page FindPage()
        {
            Element e = this;
            while (!(e is Page))
                e = e.Parent;
            return (Page)e;
        }
        public void OnPostSaved(Recipe newRecipe, Recipe old)
        {
            Page p = FindPage();
            if (p is MainTabs)
                ((MainTabs)p).RefreshPosts();
        }
        public void OnSetImageSource(ImageSource imgSource)
        {
            theImage.Source = imgSource;
        }
    }
}