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
using System.Text.RegularExpressions;

namespace RecipleaseApp.ViewModels
{
    class NewPostViewModel:INotifyPropertyChanged
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
        #region Tag
        //private Tag tag;
        //public Tag SelectedTag
        //{
        //    get { return tag; }
        //    set
        //    {
        //        tag = value;

        //       OnPropertyChanged("Tag");
        //    }
        //}
        private bool showTagError;

        public bool ShowTagError
        {
            get { return showTagError; }
            set
            {
                showTagError = false;
                OnPropertyChanged("ShowTagError");
            }
        }

        private Tag tag;
        public Tag SelectedTag
        {
            get { return tag; }
            set
            {
                tag = value;
                ValidateTag();
                OnPropertyChanged("Tag");
            }
        }

        private string tagError;

        public string TagErrorr
        {
            get { return tagError; }
            set
            {
                tagError = value;
                OnPropertyChanged("tagError");
            }
        }

        private void ValidateTag()
        {
            //this.ShowTagError = Tag.IsNullOrEmpty(tag);
            this.TagErrorr = ERROR_MESSAGES.REQUIRED_FIELD;
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

        #region Title
        private bool showTitleError;

        public bool ShowTitleError
        {
            get { return showTitleError; }
            set
            {
                showTitleError = value;
                OnPropertyChanged("ShowTitleError");
            }
        }

        private string title;

        public string Title 
        {
            get { return title; }
            set
            {
                title = value;
                ValidateTitle();
                OnPropertyChanged("Title");
            }
        }

        private string titleError;

        public string TitleError
        {
            get { return titleError; }
            set
            {
               titleError = value;
                OnPropertyChanged("TitleError");
            }
        }

        private void ValidateTitle()
        {
            this.ShowTitleError = string.IsNullOrEmpty(Title);
            this.TitleError = ERROR_MESSAGES.BAD_NAME;
        }


        #endregion

        #region Recipe Description
        private bool showRecipeDescriptionError;

        public bool ShowRecipeDescriptionError
        {
            get { return showRecipeDescriptionError; }
            set
            {
                showRecipeDescriptionError = value;
                OnPropertyChanged("ShowRecipeDescriptionError");
            }
        }

        private string recipeDescription;

        public string RecipeDescription
        {
            get { return recipeDescription; }
            set
            {
                recipeDescription = value;
                ValidateRecipeDescription();
                OnPropertyChanged("RecipeDescription");
            }
        }

        private string recipeDescriptionError;

        public string RecipeDescriptionError
        {
            get { return recipeDescriptionError; }
            set
            {
                recipeDescriptionError = value;
                OnPropertyChanged("RecipeDescriptionError");
            }
        }

        private void ValidateRecipeDescription()
        {
            this.ShowRecipeDescriptionError = string.IsNullOrEmpty(RecipeDescription);
            this.RecipeDescriptionError = ERROR_MESSAGES.BAD_NAME;
        }


        #endregion

        #region Instructions 
        private bool showInstructionsError;

        public bool ShowInstructionsError
        {
            get { return showInstructionsError; }
            set
            {
                showInstructionsError = value;
                OnPropertyChanged("ShowInstructionsError");
            }
        }

        private string instructions;

        public string Instructions
        {
            get { return instructions; }
            set
            {
                title = value;
                ValidateInstructions();
                OnPropertyChanged("Instructions");
            }
        }

        private string instructionsError;

        public string InstructionsError
        {
            get { return instructionsError; }
            set
            {
                titleError = value;
                OnPropertyChanged("InstructionsError");
            }
        }

        private void ValidateInstructions()
        {
            this.ShowTitleError = string.IsNullOrEmpty(Title);
            this.TitleError = ERROR_MESSAGES.BAD_NAME;
        }


        #endregion


        public Action<Page> NavigateToPageEvent;

        //submit command
        public ICommand SubmitCommand => new Command(OnSubmit);
        private async void OnSubmit()
        {
            RecipleaseAPIProxy proxy = RecipleaseAPIProxy.CreateProxy();
            Recipe recipe = new Recipe 
            { 
                Title=this.title,
                RecipeDescription=this.recipeDescription,
                Instructions=this.instructions,
                Tag=this.tag
            };

          

           Recipe R= await proxy.NewPostAsync(recipe);
            if (R == null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Something Happened! The Post Did Not Upload ", "Ok");
                Console.WriteLine("Something Happened! The Post Did Not Upload");
            }
            else
            {
                App app = (App)App.Current;
               // app.TheUser = R;
                Console.WriteLine("Thank You For Signing Up Tp Reciplease!");
                await App.Current.MainPage.DisplayAlert("Ok", "Great, the user was registered", "Success");
                //Page p = new ProfileView();
                //App.Current.MainPage = p;
            }
        }

        //public ICommand GoToLogInCommand => new Command(OnGoToLogInSubmit);
        //private async void OnGoToLogInSubmit()
        //{
        //    Page p = new ProfileView();
        //    App.Current.MainPage = p;
        //}

    }
}
