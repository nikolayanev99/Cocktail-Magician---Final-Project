using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Web.Models
{
    public class IngredientViewModel
    {
        public int Id { get; set; }

        [DisplayName("Ingredient Name")]
        [Required]
        [StringLength(35, ErrorMessage = "The Name of ingredient cannot exceed 35 characters")]
        public string Name { get; set; }
    }
}
