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
    class ExploreViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<Recipe> allRecipes;
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

        public ExploreViewModel()
        {
            this.SearchTerm = string.Empty;
            InitRecipes();
        }

        private void InitRecipes()
        {
            isRefreshing = true;
            App theApp = (App)App.Current;
            this.allRecipes = (List<Recipe>)theApp.TheUser.Recipes;

            this.FilteredRecipes = new ObservableCollection<Recipe>(this.allRecipes.OrderBy(R => R.Title));
            SearchTerm = string.Empty;
            IsRefreshing = false;

        }

        //commands
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
                    Ingredients = chosenRecipe.RecipeIngs,
                    //FrontImageUrl = chosenRecipe.
                    Tag = chosenRecipe.Tag,
                    // LastImageUrl = chosenRecipe,
                    Time = chosenRecipe.DateOfUpload
                 


                };
                recipePage.BindingContext = recipeContext;
                recipePage.Title = recipeContext.Title;
                NavigateToPageEvent(recipePage);
               // OnRefresh();



            }
        }
        #region Events
        //Events
        //This event is used to navigate to the monkey page
        public Action<Page> NavigateToPageEvent;
        #endregion
        #endregion
        #region Delete Contact
        //public ICommand DeleteContact => new Command<UserContact>(OnDeleteContact);
        //public async void OnDeleteContact(UserContact uc)
        //{
        //    ContactsAPIProxy proxy = ContactsAPIProxy.CreateProxy();
        //    bool success = await proxy.RemoveContact(uc);
        //    if (success)
        //    {
        //        this.allContacts.Remove(uc);
        //        this.FilteredContacts.Remove(uc);
        //    }
        //    else
        //    {
        //        await App.Current.MainPage.DisplayAlert("שגיאה", "שגיאה במחיקת איש קשר", "בסדר", FlowDirection.RightToLeft);
        //    }
        //}
        #endregion
        #region Add New Contact
        //public ICommand AddContact => new Command(OnAddContact);
        //public async void OnAddContact()
        //{
        //    App theApp = (App)App.Current;
        //    AddContactViewModel vm = new AddContactViewModel();
        //    vm.ContactUpdatedEvent += OnContactAdded;
        //    Page p = new Views.AddContact(vm);
        //    await theApp.MainPage.Navigation.PushAsync(p);
        //}
        ////This event is fired by the AddContact view model object and send the old contact to be removed and new contact to be added
        //public void OnContactAdded(UserContact newUc, UserContact oldUc)
        //{
        //    //Change the Phone type objects to be the same instance of the PhoneTypes in App level
        //    //This is a must in order to send the server the same objects
        //    foreach (ContactPhone cp in newUc.ContactPhones)
        //        cp.PhoneType = PhoneTypes.Where(pt => pt.TypeId == cp.PhoneTypeId).FirstOrDefault();

        //    //Add the new contact, remove the old one from both lists and refresh the filtered list
        //    this.allContacts.Remove(oldUc);
        //    this.allContacts.Add(newUc);
        //    this.FilteredContacts.Remove(oldUc);
        //    OnTextChanged(SearchTerm);
        //}

        #endregion
        #region Update Existing Contact
        //public ICommand UpdateContact => new Command<UserContact>(OnUpdateContact);
        //public async void OnUpdateContact(UserContact uc)
        //{
        //    if (uc != null)
        //    {
        //        App theApp = (App)App.Current;
        //        AddContactViewModel vm = new AddContactViewModel(uc);
        //        vm.ContactUpdatedEvent += OnContactAdded;
        //        Page p = new Views.AddContact(vm);
        //        await theApp.MainPage.Navigation.PushAsync(p);
        //        if (ClearSelection != null)
        //            ClearSelection();
        //    }
        //}

        //public event Action ClearSelection;
        #endregion
    }



}


