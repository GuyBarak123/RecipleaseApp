using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Essentials;
using System.Windows.Input;
using Xamarin.Forms;
using RecipleaseApp.Services;
using RecipleaseApp.Models;
using RecipleaseApp.Views;
using System.Collections.ObjectModel;
using System.Linq;
namespace RecipleaseApp.ViewModels
{
    class NewPostViewModel
    {
        #region tabbed bar commands
        public Action<Page> NavigateToPageEvent;
        public ICommand GoToExplorePageCommand => new Command(OnGoToExplorePageSubmit);
        private async void OnGoToExplorePageSubmit()
        {
            Page p = new ExploreView();
            App.Current.MainPage = p;

        }
        public ICommand GoToProfileCommand => new Command(OnGoToProfileSubmit);
        private async void OnGoToProfileSubmit()
        {
            Page p = new ProfileView();
            App.Current.MainPage = p;

        }
        #endregion
    }
}
