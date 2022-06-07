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
using RecipleaseApp;

namespace RecipleaseApp.ViewModels
{
    class UpdateRecipeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //public Action<Page> NavigateToPageEvent;
        //public string Title { get; set; }
        //public string RecipeDescription { get; set; }
        //public string Instructions { get; set; }
        //public ObservableCollection<Comment> Comments { get; set; }
        //public string ImgSource { get; set; }
        //public Tag Tag { get; set; }
        //public int RecipeId { get; set; }

        //public DateTime? Time { get; set; }
        //public Recipe currentRecipe { get; set; }
        public static class ERROR_MESSAGES
        {
            public const string REQUIRED_FIELD = "This is a required field";
            public const string BAD_EMAIL = "Invalid email";
            public const string SHORT_PASS = "The password must contain at least 10 characters";
            public const string BAD_NAME = "Invalid Name";

        }

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
                instructions = value;
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
                instructionsError = value;
                OnPropertyChanged("InstructionsError");
            }
        }

        private void ValidateInstructions()
        {
            this.ShowInstructionsError = string.IsNullOrEmpty(Instructions);
            this.InstructionsError = ERROR_MESSAGES.BAD_NAME;
        }


        #endregion
        #region Photo Origin
        private string recipeImgSrc;

        public string RecipeImgSrc
        {
            get => recipeImgSrc;
            set
            {
                recipeImgSrc = value;
                OnPropertyChanged("RecipeImgSrc");
            }
        }
        private const string DEFAULT_PHOTO_SRC = "defaultphoto.jpg";
        //private readonly object App;
        #endregion
        #region server status
        private string serverStatus;
        public string ServerStatus
        {
            get { return serverStatus; }
            set
            {
                serverStatus = value;
                OnPropertyChanged("ServerStatus");
            }
        }
        #endregion
        // This contact is a reference to the updated or new created contact
        private Recipe theRecipe;
        public event Action<Recipe, Recipe> RecipeUpdatedEvent;
        public Action<Page> NavigateToPageEvent;

        //submit command
        public ICommand UpdateCommand => new Command<Recipe>(UpdateRecipe);
        private async void UpdateRecipe(Recipe recp)
        {
            App app = (App)App.Current;


            RecipleaseAPIProxy proxy = RecipleaseAPIProxy.CreateProxy();
            Recipe recipe = new Recipe
            {
            };
            ServerStatus = "מתחבר לשרת...";
            await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));


            Recipe newRC = await proxy.NewPostAsync(recipe);


            if (newRC == null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Saving The Recipe Failed", "OK");
                //await App.Current.MainPage.Navigation.PopModalAsync();
            }
            else
            {
                app.TheUser.Recipes.Add(newRC);

               

                
            }




            //close the message and add contact windows!

            //await App.Current.MainPage.Navigation.PopAsync();
            await App.Current.MainPage.Navigation.PopModalAsync();
        }


        // private Recipe theRecipe;
        // public event Action<Recipe, Recipe> RecipeUpdatedEvent;
        //// public Action<Page> NavigateToPageEvent;

        // //submit command


        public UpdateRecipeViewModel(Recipe recp)
        {
            this.theRecipe = recp;
        }

        //public ICommand UpdateCommand => new Command<Recipe>(UpdateRecipe);
        //private async void UpdateRecipe(Recipe recp)
        //{
        //    App app = (App)App.Current;

        //    RecipleaseAPIProxy proxy = RecipleaseAPIProxy.CreateProxy();
        //    Recipe recipe = new Recipe
        //    {
        //        RecipeId=recp.RecipeId,
        //        UserId = app.TheUser.UserId,
        //        RecipeDescription=recp.RecipeDescription,
        //        Title=recp.Title,
        //        Instructions=recp.Instructions

        //    };
        //}



    }









}

