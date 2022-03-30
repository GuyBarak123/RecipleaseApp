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
    class LikedRecipesViewModel
    {
        public LikedRecipesViewModel()
        {
            //AllRecipesList = new List<Recipe>();
            LikedRecipesView = new ObservableCollection<Recipe>();
            //SelectionChanged = new Command(PostView);

        }


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Full Posts List
        private List<Recipe> fullRecipesList;
        public List<Recipe> FullRecipesList
        {
            get
            {
                return this.fullRecipesList;
            }

            set
            {
                if (this.fullRecipesList != value)
                {
                    this.fullRecipesList = value;
                    OnPropertyChanged("FullRecipesList");
                }
            }
        }
        #endregion

        #region Favorite Posts List
        private ObservableCollection<Recipe> likedRecipesView;
        public ObservableCollection<Recipe> LikedRecipesView
        {
            get
            {
                return this.likedRecipesView;
            }

            set
            {
                if (this.likedRecipesView != value)
                {
                    this.likedRecipesView = value;
                    OnPropertyChanged("LikedRecipesView");
                }
            }
        }
        #endregion

        #region Selected Post
        private object selectedRecipe;
        public Recipe SelectedRecipe
        {
            get => (Recipe)selectedRecipe;
            set
            {
                if (this.selectedRecipe != value)
                {
                    this.selectedRecipe = value;
                    OnPropertyChanged("SelectedRecipe");
                }
            }
        }
        #endregion

        #region Delete Button
        public Command RemoveButton => new Command<Recipe>(RemoveFromLiked);
        public async void RemoveFromLiked(Recipe selected)
        {

            bool result = await App.Current.MainPage.DisplayAlert("Are You Sure?", null, "Yes", "Cancel", FlowDirection.RightToLeft);
            if (result)
            {

                ExploreViewModel ep = new ExploreViewModel();
                ep.AddToLikedRecipes(selected);

                RecipleaseAPIProxy proxy = RecipleaseAPIProxy.CreateProxy();
                bool b = await proxy.AddToLikedRecipes(selected.RecipeId);
                if(b)
                {
                    LikedRecipesView.Remove(selected);
                    App app = (App)App.Current;
                    User u = app.TheUser;
                    Like L = u.Likes.Where(t => t.RecipeId == selected.RecipeId).FirstOrDefault();
                    if (L!=null)
                    {
                        u.Likes.Remove(L);
                        Message = "You Dont Have Recipes In Liked";
                    }
                }

                else
                {
                    b = false;
                }
                
               
            }
        }

        #endregion

        //#region Logo Command
        //public Command LogoCommand => new Command(Logo);
        //public async void Logo()
        //{
        //    App app = (App)App.Current;
        //    await app.MainPage.Navigation.PopToRootAsync();
        //    NavigationPage nv = (NavigationPage)app.MainPage;
        //    await nv.PopToRootAsync();
        //    MainTab mt = (MainTab)nv.CurrentPage;
        //    mt.SwitchToHomeTab();
        //}
        //#endregion

        #region Message
        private string message;
        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                if (this.message != value)
                {
                    this.message = value;
                    OnPropertyChanged("Message");
                }
            }
        }
        #endregion

        //public async void Operate()
        //{
        //    await InitPosts();
        //    await GetFavoritePosts();
        }
     //private async Task InitPosts()
        //{
        //    GuitarsAndMoreAPIProxy proxy = GuitarsAndMoreAPIProxy.CreateProxy();
        //    List<Post> pList = await proxy.GetListOfPostsAsync();
        //    if (pList != null)
        //    {
        //        FullPostsList.Clear();
        //        foreach (Post p in pList)
        //        {
        //            FullPostsList.Add(p);
        //        }
        //    }
        //}

        //private async Task GetFavoritePosts()
        //{
        //    App app = (App)App.Current;
        //    User u = app.CurrentUser;
        //    ICollection<UserFavoritePost> checkList = u.UserFavoritePosts;
        //    FavoritePostsList.Clear();
        //    if (checkList.Count() > 0)
        //    {
        //        foreach (Post p in FullPostsList)
        //        {
        //            foreach (UserFavoritePost ufp in checkList)
        //                if (ufp.PostId == p.PostId)
        //                    FavoritePostsList.Add(p);
        //        }
        //    }
        //    if (checkList.Count() == 0)
        //        Message = "אין לך מודעות במועדפים";


        //}

        //public ICommand SelectionChanged { get; set; }
        //public void PostView()
        //{
        //    if (SelectedRecipe != null)
        //    {
        //        App app = (App)App.Current;
        //        app.MainPage.Navigation.PushAsync(new PostView(SelectedPost));
        //        SelectedPost = null;
        //    }
        //}



    }
