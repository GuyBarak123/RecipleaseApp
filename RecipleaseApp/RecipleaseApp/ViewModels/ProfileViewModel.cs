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
    class ProfileViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        
        private List<Recipe> allRecipes;
      // public Action<Page> NavigateToPageEvent;
        private ObservableCollection<Recipe> filteredRecipes;
        public ObservableCollection<Recipe> FilteredRecipes
        {
            get
            {
                return this.filteredRecipes;
            }
            set
            {
                if (this.filteredRecipes != value)
                {

                    this.filteredRecipes = value;
                    OnPropertyChanged("FilteredRecipes");
                }
            }
        }
        private string searchTerm;
        public string SearchTerm
        {
            get
            {
                return this.searchTerm;
            }
            set
            {
                if (this.searchTerm != value)
                {

                    this.searchTerm = value;
                    OnTextChanged(value);
                    OnPropertyChanged("SearchTerm");
                }
            }
        }
        
        
        private async void InitRecipes()
        {
            isRefreshing = true;
            App theApp = (App)App.Current;
            User u = theApp.TheUser;
            RecipleaseAPIProxy proxy = RecipleaseAPIProxy.CreateProxy();

            this.allRecipes = await proxy.GetRecepiesAsync();

            this.allRecipes = this.allRecipes.Where(r => r.UserId == u.UserId).ToList();
            

            this.FilteredRecipes = new ObservableCollection<Recipe>(this.allRecipes.OrderBy(R => R.Title));
            SearchTerm = string.Empty;
            IsRefreshing = false;

        }

        

        #region Name
        private string name;
        public string Name
        {
            get => name;
            set
            {
               name = value;
                OnPropertyChanged("Name");
            }
        }
        #endregion
        #region Email
        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }
        #endregion

        #region Password
        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        #endregion
        #region Recipe
        private string recipe;
        public string Recipe
        {
            get => recipe;
            set
            {
                recipe = value;
                OnPropertyChanged("Password");
            }
        }
        #endregion
        #region Constructor
        public ProfileViewModel()
        {
            this.SearchTerm = String.Empty;
            InitRecipes();


            App theApp = (App)App.Current;
            User TheUser = theApp.TheUser;

            this.Email = TheUser.Email;
            this.Password = TheUser.Password;
            this.name = TheUser.Name;
          
            //set the path url to the contact photo
           RecipleaseAPIProxy proxy = RecipleaseAPIProxy.CreateProxy();
           
            
        }
        #endregion
        private Recipe selectedRecipe;
        public Recipe SelectedRecipe
        {
            get
            {
                return this.selectedRecipe;
            }
            set
            {
                if (this.selectedRecipe != value)
                {
                    this.selectedRecipe = value;
                    OnPropertyChanged("SelectedRecipe");

                }
            }
        }
        #region Delete Contact
        public ICommand DeleteContact => new Command<Recipe>(OnDeleteContact);
        public async void OnDeleteContact(Recipe R)
        {
            RecipleaseAPIProxy proxy = RecipleaseAPIProxy.CreateProxy();
            bool success = await proxy.RemoveRecipe(R);
            if (success)
            {
                this.allRecipes.Remove(R);
                this.filteredRecipes.Remove(R);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "An Error Happened While Deleting", "Ok", FlowDirection.RightToLeft);
            }
        }
        #endregion
        #region Search
        public void OnTextChanged(string search)
        {
            //Filter the list of contacts based on the search term
            if (this.allRecipes == null)
                return;
            if (String.IsNullOrWhiteSpace(search) || String.IsNullOrEmpty(search))
            {
                foreach (Recipe R in this.allRecipes)
                {
                    if (!this.FilteredRecipes.Contains(R))
                        this.FilteredRecipes.Add(R);


                }
            }
            else
            {
                foreach (Recipe R in this.allRecipes)
                {
                    string RecipeString = $"{R.Title}|{R.User}|{R.Title.IndexOf(searchTerm)}";



                    if (!this.FilteredRecipes.Contains(R) &&
                       RecipeString.Contains(search))
                        this.FilteredRecipes.Add(R);
                    else if (this.FilteredRecipes.Contains(R) &&
                        !RecipeString.Contains(search))
                        this.FilteredRecipes.Remove(R);
                }
            }
            this.FilteredRecipes = new ObservableCollection<Recipe>(this.FilteredRecipes.OrderBy(R => R.Title));
        }




        #endregion
        #region Refresh
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                if (this.isRefreshing != value)
                {
                    this.isRefreshing = value;
                    OnPropertyChanged(nameof(IsRefreshing));
                }
            }
        }
        public ICommand RefreshCommand => new Command(OnRefresh);
        public void OnRefresh()
        {
            InitRecipes();
        }
        #endregion
        #region selected recipe
        //Selection changed 
        public ICommand SelctionChanged => new Command<Object>(OnSelectionChanged);
        public void OnSelectionChanged(Object obj)
        {
            if (obj is Recipe)
            {
                Recipe chosenRecipe = (Recipe)obj;
                Page recipePage = new ShowRecipeView();
                ShowRecipeViewModel recipeContext = new ShowRecipeViewModel
                {
                    Title = chosenRecipe.Title,
                    RecipeDescription = chosenRecipe.RecipeDescription,
                    Instructions = chosenRecipe.Instructions,
                    Comments = new ObservableCollection<Comment>(chosenRecipe.Comments),
                    //FrontImageUrl = chosenRecipe.
                    Tag = chosenRecipe.Tag,
                    // LastImageUrl = chosenRecipe,
                    Time = chosenRecipe.DateOfUpload



                };
                recipePage.BindingContext = recipeContext;
                recipePage.Title = recipeContext.Title;
                NavigateToPageEvent(recipePage);
                SelectedRecipe = null;
                // OnRefresh();



            }


        }
        #region Events
        //Events
        //This event is used to navigate to the monkey page
        public Action<Page> NavigateToPageEvent;
        #endregion
        #endregion
        //#region Update Existing Recipe
        //public ICommand UpdateRecipe => new Command<Recipe>(OnUpdateRecipe);
        //public async void OnUpdateRecipe(Recipe R)
        //{
        //    if (R != null)
        //    {
        //        App theApp = (App)App.Current;
        //        NewPostViewModel vm = new NewPostViewModel(R);
        //        vm.ContactUpdatedEvent += OnContactAdded;
        //        Page p = new Views.AddContact(vm);
        //        await theApp.MainPage.Navigation.PushAsync(p);
        //        if (ClearSelection != null)
        //            ClearSelection();
        //    }
        //}

        //public event Action ClearSelection;
        //#endregion

    }
}

