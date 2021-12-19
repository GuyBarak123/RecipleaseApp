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
    class ProfileViewModel
    {
        public Action<Page> NavigateToPageEvent;
        #region tabbed bar commands
        public ICommand GoToExplorePageCommand => new Command(OnGoToExplorePageSubmit);
        private async void OnGoToExplorePageSubmit()
        {
            Page p = new ExploreView();
            App.Current.MainPage = p;

        }
        public ICommand GoToNewPostCommand => new Command(OnGoToNewPostSubmit);
        private async void OnGoToNewPostSubmit()
        {
            Page p = new ProfileView();
            App.Current.MainPage = p;

        }
        #endregion

    }
}
