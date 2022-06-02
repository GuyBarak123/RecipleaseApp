using System;
using System.Collections.Generic;
using System.Text;

namespace RecipleaseApp.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public int? RecipeId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
