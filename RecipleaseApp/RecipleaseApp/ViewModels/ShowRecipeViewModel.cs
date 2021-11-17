using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RecipleaseApp.ViewModels
{
    class ShowRecipeViewModel : INotifyPropertyChanged
    {
        public string Title { get; set; }
        public string RecipeDescription { get; set; }
        public string Instructions { get; set; }
        public string Ingredients { get; set; }
        public string FrontImageUrl { get; set; }
        public string Tag { get; set; }
        public string LastImageUrl { get; set; }
        public DateTime Time { get; set; }




        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


    }
}
