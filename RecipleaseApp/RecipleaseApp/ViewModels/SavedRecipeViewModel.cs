using System;
using System.Collections.Generic;
using System.Text;
using RecipleaseApp.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using RecipleaseApp.Services;
using RecipleaseApp.Views;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Input;


namespace RecipleaseApp.ViewModels
{
    class SavedRecipeViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

       
    }
}
