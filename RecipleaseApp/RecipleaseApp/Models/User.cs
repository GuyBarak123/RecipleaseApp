using System;
using System.Collections.Generic;
using System.Text;

namespace RecipleaseApp.Models
{
    public partial class User
    {
   
        
       
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int? GenderId { get; set; }
        public int? TagId { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime? SignUpTime { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual Tag Tag { get; set; }
        public virtual Gender Gender { get; set; }

    }
}
