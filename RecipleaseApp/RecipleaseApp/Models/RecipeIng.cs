using System;
using System.Collections.Generic;
using System.Text;

namespace RecipleaseApp.Models
{
    public partial class RecipeIng
    {
        public int RecipeIngId { get; set; }
        public int? RecipeId { get; set; }
        public int? IngridientId { get; set; }
        public double Amount { get; set; }
        public int? MeasurementId { get; set; }

        public Ingridient Ingridient { get; set; }
        public Measurement Measurement { get; set; }
        public Recipe Recipe { get; set; }
    }
}
