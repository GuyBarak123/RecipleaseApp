using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Essentials;
using System.Windows.Input;
using Xamarin.Forms;
using RecipleaseApp.Services;
using RecipleaseApp.Models;

namespace RecipleaseApp.ViewModels
{
    class LogInViewModel : INotifyPropertyChanged
    { 
    
        public event PropertyChangedEventHandler PropertyChanged;

        //Name
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }



        //Password
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }


        //error message
        private string errormes;
        public string ErrorMes
        {
            get { return errormes; }

            set
            {
                errormes = "something went wrong! please try again";
            }
        }


        //Submit Command 
        public ICommand SubmitCommand => new Command(OnSubmit);
        private async void OnSubmit()
        {
            RecipleaseAPIProxy proxy = RecipleaseAPIProxy.CreateProxy();
            User u = await proxy.LoginAsync(Email, Password);
            if (u == null)
            {

            }
            else
            {
                App app = (App) App.Current;
                app.TheUser = u;
                //Move to next screen
                //app.MainPage = ....
            }
        }




        //Go to sign up command 

    }
}
