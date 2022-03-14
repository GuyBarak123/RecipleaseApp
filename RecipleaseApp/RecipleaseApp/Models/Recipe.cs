using System;
using System.Collections.Generic;
using System.Text;
using RecipleaseApp.Services;
namespace RecipleaseApp.Models
{
    public partial class Recipe
    {
        public Recipe()
        {
            //Comments = new HashSet<Comment>();
            //Likes = new HashSet<Like>();
            //RecipeIngs = new HashSet<RecipeIng>();
        }

        public int RecipeId { get; set; }
        public int? UserId { get; set; }
        public string Title { get; set; }
        public string RecipeDescription { get; set; }
        public string Instructions { get; set; }
        public int? TagId { get; set; }
        public DateTime? DateOfUpload { get; set; }

        public virtual Tag Tag { get; set; }
        public virtual User User { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Like> Likes { get; set; }
        public virtual List<RecipeIng> RecipeIngs { get; set; }

        //additional properties only on app side
        public string ImgSource
        {
            get
            {
                RecipleaseAPIProxy proxy = RecipleaseAPIProxy.CreateProxy();
                string str = $"{proxy.GetPhotoUri()}{this.RecipeId}.jpg";
                return str;
            }
        }
    }
}
