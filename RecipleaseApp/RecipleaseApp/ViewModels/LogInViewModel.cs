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

namespace RecipleaseApp.ViewModels
{
    class LogInViewModel : INotifyPropertyChanged
    { 
    
        public event PropertyChangedEventHandler PropertyChanged;

        //Email
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
                await App.Current.MainPage.DisplayAlert("Error", "Something Happened! Sign Up Did Not Work ", "Ok");
            }
            else
            {
                App app = (App) App.Current;
                app.TheUser = u;
                //Move to next screen
                app.MainPage = new ExploreView();
            }
        }




        //Go to sign up command 
        public ICommand GoToSignUpCommand => new Command(OnGoToSignUpSubmit);
        private async void OnGoToSignUpSubmit()
        {
            Page p = new SIgnUpView();
            App.Current.MainPage = p;

        }

    }
}
