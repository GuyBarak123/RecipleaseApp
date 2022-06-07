using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using RecipleaseApp.Models;
using RecipleaseApp.Services;

namespace RecipleaseApp.ViewModels
{
    class ShowRecipeViewModel : INotifyPropertyChanged
    {
        public Action<Page> NavigateToPageEvent;
        public string Title { get; set; }
        public string RecipeDescription { get; set; }
        public string Instructions { get; set; }
        public ObservableCollection<Comment> Comments{ get; set; }
        public string ImgSource { get; set; }
        public Tag Tag { get; set; }
        public int RecipeId { get; set; }
       
        public DateTime? Time { get; set; }

        public static class ERROR_MESSAGES
        {
            public const string REQUIRED_FIELD = "This is a required field";
            public const string BAD_EMAIL = "Invalid email";
            public const string SHORT_PASS = "The password must contain at least 10 characters";
            public const string BAD_NAME = "Invalid Name";

        }
        #region Recipe Description
        private bool showCommentError;

        public bool ShowCommentError
        {
            get { return showCommentError; }
            set
            {
                showCommentError = value;
                OnPropertyChanged("ShowCommentError");
            }
        }

        private string comment;

        public string Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                ValidateComment();
                OnPropertyChanged("Comment");
            }
        }

        private string commentError;

        public string CommentError
        {
            get { return commentError; }
            set
            {
                commentError = value;
                OnPropertyChanged("CommentError");
            }
        }

        private void ValidateComment()
        {
            this.ShowCommentError = string.IsNullOrEmpty(Comment);
            this.CommentError = ERROR_MESSAGES.BAD_NAME;
        }


        #endregion
        public ICommand SubmitCommand => new Command(OnSubmit);
        private async void OnSubmit()
        {
            App app = (App)App.Current;


            RecipleaseAPIProxy proxy = RecipleaseAPIProxy.CreateProxy();
            Comment comment = new Comment
            {
                Content = this.comment,
                RecipeId = this.RecipeId,
                UserId = ((App)App.Current).TheUser.UserId,
                User = app.TheUser
            };
          





        
            await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));

            bool result  = await proxy.AddCommentToRecipe(comment);
         

            if (!result)
            {
                await App.Current.MainPage.DisplayAlert("Error", "comment Failed", "OK");
                //await App.Current.MainPage.Navigation.PopModalAsync();
            }
            else
            {
                Comments.Add(comment);
            }




            //close the message and add contact windows!

            //await App.Current.MainPage.Navigation.PopAsync();
            await App.Current.MainPage.Navigation.PopModalAsync();
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


    }
}
