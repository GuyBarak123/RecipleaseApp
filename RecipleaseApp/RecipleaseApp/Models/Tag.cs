using System;
using System.Collections.Generic;
using System.Text;

namespace RecipleaseApp.Models
{
    public partial class Tag
    {
        public Tag()
        {
        }
        public int TagId { get; set; }
        public string TagName { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }

    }
}
