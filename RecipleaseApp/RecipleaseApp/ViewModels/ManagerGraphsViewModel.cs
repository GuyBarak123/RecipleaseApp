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
    class ManagerGraphsViewModel
    {
        public List<User> AllUsers { get; set; }
        

        public void OnRefresh()
        {

        }
    }
}
