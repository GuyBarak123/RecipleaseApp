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
        public ICommand SubmitCommand => new Command(OnSubmit);
        private async void OnSubmit()
        {
            RecipleaseAPIProxy proxy = RecipleaseAPIProxy.CreateProxy();
            Recipe recipe = new Recipe 
            { 
                Title=this.title,
                RecipeDescription=this.recipeDescription,
                Instructions=this.instructions,
                TagId = this.tag.TagId
            };
            ServerStatus = "מתחבר לשרת...";
            await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));
            RecipleaseAPIProxy reciproxy = RecipleaseAPIProxy.CreateProxy();
            await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));
          
            Recipe newRC = await proxy.UpdateRecipe(this.theRecipe);
        

            //if (newRC == null)
            //{
            //    await App.Current.MainPage.DisplayAlert("Error", "Saving The Recipe Failed", "OK");
            //    await App.Current.MainPage.Navigation.PopModalAsync();
            //}
            //else
            //{
                if (this.imageFileResult != null)
                {
                    ServerStatus = "Uploading......";

                    bool success = await proxy.UploadImage(new FileInfo()
                    {
                        Name = this.imageFileResult.FullPath
                    }, $"{newRC.RecipeId}.jpg");
                }
            //}
            ServerStatus = "Saving Data...";
            //if someone registered to get the contact added event, fire the event
            if (this.RecipeUpdatedEvent != null)
            {
                this.RecipeUpdatedEvent(newRC, this.theRecipe);
            }
            //close the message and add contact windows!

            await App.Current.MainPage.Navigation.PopAsync();
            await App.Current.MainPage.Navigation.PopModalAsync();



            Recipe R = await proxy.NewPostAsync(recipe);
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
                await App.Current.MainPage.DisplayAlert("Ok", "Great, The recipe was posted! no need to submit again", "Success");

               // Page p = new ProfileView();
                //App.Current.MainPage = p;
            }
        }

        //public ICommand GoToLogInCommand => new Command(OnGoToLogInSubmit);
        //private async void OnGoToLogInSubmit()
        //{
        //    Page p = new ProfileView();
        //    App.Current.MainPage = p;
        //}

        FileResult imageFileResult;
        public event Action<ImageSource> SetImageSourceEvent;
        public ICommand PickImageCommand => new Command(OnPickImage);
        public async void OnPickImage()
        {
            FileResult result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions()
            {
                Title = "pick a picture"
            });

            if (result != null)
            {
                this.imageFileResult = result;

                var stream = await result.OpenReadAsync();
                ImageSource imgSource = ImageSource.FromStream(() => stream);
                if (SetImageSourceEvent != null)
                    SetImageSourceEvent(imgSource);
            }
        }

        ///The following command handle the take photo button
        public ICommand CameraImageCommand => new Command(OnCameraImage);
        public async void OnCameraImage()
        {
            var result = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions()
            {
                Title = "צלם תמונה"
            });

            if (result != null)
            {
                this.imageFileResult = result;
                var stream = await result.OpenReadAsync();
                ImageSource imgSource = ImageSource.FromStream(() => stream);
                if (SetImageSourceEvent != null)
                    SetImageSourceEvent(imgSource);
            }
        }

    }
}
