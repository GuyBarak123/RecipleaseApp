using System;
using System.Collections.Generic;
using System.Text;

namespace RecipleaseApp.Models
{
    public partial class Saved
    {
        public int? RecipeId { get; set; }
        public int? UserId { get; set; }

        public virtual Recipe Recipe { get; set; }
        public virtual User User { get; set; }

        //
        public virtual ICollection<Like> Likes { get; set; }
    }
}
