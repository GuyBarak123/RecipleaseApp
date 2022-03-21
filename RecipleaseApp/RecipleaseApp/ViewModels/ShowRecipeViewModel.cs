using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using RecipleaseApp.Models;

namespace RecipleaseApp.ViewModels
{
    class ShowRecipeViewModel : INotifyPropertyChanged
    {
        public Action<Page> NavigateToPageEvent;
        public string Title { get; set; }
        public string RecipeDescription { get; set; }
        public string Instructions { get; set; }
        public List<RecipeIng> Ingredients { get; set; }
        public string ImgSource { get; set; }
        public Tag Tag { get; set; }
       
        public DateTime? Time { get; set; }




        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


    }
}
