using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Essentials;
using System.Windows.Input;
using Xamarin.Forms;


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

        //Submit Command 
        public ICommand SubmitCommand => new Command(OnSubmit);
        private void OnSubmit()
        {

        }




        //Go to sign up command 

    }
}
