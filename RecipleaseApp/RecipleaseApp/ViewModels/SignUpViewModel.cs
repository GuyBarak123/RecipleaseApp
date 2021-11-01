using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Essentials;
using System.Windows.Input;
using Xamarin.Forms;
using System.Text.RegularExpressions;
using RecipleaseApp.Services;
using RecipleaseApp.Models;
using RecipleaseApp.Views;

namespace RecipleaseApp.ViewModels
{
    class SignUpViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static class ERROR_MESSAGES
        {
            public const string REQUIRED_FIELD = "This is a required field";
            public const string BAD_EMAIL = "Invalid email";
            public const string SHORT_PASS = "The password must contain at least 10 characters";
            public const string BAD_NAME = "Invalid Name";

        }

        #region Email
    

        private bool showEmailError;

        public bool ShowEmailError
        {
            get { return showEmailError; }
            set
            {
                showEmailError = value;
                OnPropertyChanged("ShowEmailError");
            }
        }

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

        private string emailError;

        public string EmailError
        {
            get { return emailError; }
            set
            {
                emailError = value;
                OnPropertyChanged("EmailError");
            }
        }

        

        private void ValidateEmail()
        {
            this.ShowEmailError = string.IsNullOrEmpty(Email);
            if (!this.ShowEmailError)
            {
                if (!Regex.IsMatch(this.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                {
                    this.ShowEmailError = true;
                    this.EmailError = ERROR_MESSAGES.BAD_EMAIL;
                }
            }
            else
                this.EmailError = ERROR_MESSAGES.REQUIRED_FIELD;
        }

        #endregion
        #region Name
        private bool showNameError;

        public bool ShowNameError
        {
            get { return showNameError; }
            set
            {
                showNameError = value;
                OnPropertyChanged("ShowNameError");
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                ValidateName();
                OnPropertyChanged("Name");
            }
        }

        private string nameError;

        public string NameError
        {
            get { return nameError; }
            set
            {
                nameError = value;
                OnPropertyChanged("NameError");
            }
        }

        private void ValidateName()
        {
            this.ShowNameError = string.IsNullOrEmpty(Name);
            this.NameError = ERROR_MESSAGES.BAD_NAME;
        }


        #endregion

        #region Password
        private bool showPasswordError;

        public bool ShowPasswordError
        {
            get { return showPasswordError; }
            set
            {
                showPasswordError = value;
                OnPropertyChanged("ShowPasswordError");
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                ValidatePassword();
                OnPropertyChanged("Password");
            }
        }

        private string passwordError;

        public string PasswordError
        {
            get { return passwordError; }
            set
            {
                passwordError = value;
                OnPropertyChanged("PasswordError");
            }
        }

        private void ValidatePassword()
        {
            this.ShowPasswordError = string.IsNullOrEmpty(Password);
            if (!this.ShowPasswordError)
            {
                if (this.Password.Length < 10)
                {
                    this.ShowPasswordError = true;
                    this.PasswordError = ERROR_MESSAGES.SHORT_PASS;
                }
            }
            else
                this.PasswordError = ERROR_MESSAGES.REQUIRED_FIELD;
        }

        #endregion

        #region Gender
        private Gender gender;
        public Gender SelectedGender
        {
            get => gender;

            set
            {
                if (gender != value)
                {

                    gender = value;
                    OnPropertyChanged("SelectedGender");
                }
            }
        }
        

        //private bool showGenderError;

        //public bool ShowGenderError
        //{
        //    get { return showGenderError; }
        //    set
        //    {
        //        showGenderError = value;
        //        OnPropertyChanged("ShowGenderError");
        //    }
        //}

        //private string genderr;

        //public string Genderr
        //{
        //    get { return genderr; }
        //    set
        //    {
        //        name = value;
        //        ValidateGenderr();
        //        OnPropertyChanged("Genderr");
        //    }
        //}

        //private string genderError;

        //public string GenderError
        //{
        //    get { return genderError; }
        //    set
        //    {
        //        genderError = value;
        //        OnPropertyChanged("gendrError");
        //    }
        //}

        //private void ValidateGenderr()
        //{
        //    this.ShowNameError = string.IsNullOrEmpty(Name);
        //    this.NameError = ERROR_MESSAGES.REQUIRED_FIELD;
        //}



        #endregion

        #region Tag
        private Tag tag;
        public Tag SelectedTag
        {
            get { return tag; }
            set
            {
                tag = value;
                
               OnPropertyChanged("Tag");
            }
        }

        #endregion
        #region Tags


        public List<Tag> Tags
        {
            get
            {
                App app = (App)App.Current;
                return app.Lookups.Tags;
            }
        }


        #endregion

        #region IsAdmin


        #endregion

        #region Genders


        public List<Gender> Genders
        {
            get 
            {
                App app = (App)App.Current;
                return app.Lookups.Genders;
            }
         }

  
        #endregion

        //submit command
        public ICommand SubmitCommand => new Command(OnSubmit);
        private async void OnSubmit()
        {
            RecipleaseAPIProxy proxy = RecipleaseAPIProxy.CreateProxy();
            User use = new User
            {
                Email = this.email,
                Name = this.name,
                Password = this.password,
                GenderId = this.SelectedGender.GenderId,
                TagId=this.SelectedTag.TagId
            };

            User u = await proxy.SignUpAsync(use);
            if (u == null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Something Happened! Sign Up Did Not Work ", "Ok");
                Console.WriteLine("Something Happened! Sign Up Did Not Work ");
            }
            else
            {
                App app = (App)App.Current;
                app.TheUser = u;
                Console.WriteLine("Thank You For Signing Up Tp Reciplease!");
                await App.Current.MainPage.DisplayAlert("Ok", "Great, the user was registered", "Success");
                // page p=new HomePage();
                //app.MainPage= new NavigationPage(p);
            }
        }

        public ICommand GoToLogInCommand => new Command(OnGoToLogInSubmit);
        private async void OnGoToLogInSubmit()
        {
            Page p = new LogInView();
            App.Current.MainPage = p;
        }
    }
}
