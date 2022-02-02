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
    class PopUp_Weight:INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Action<Page> NavigateToPageEvent;
    }
}
