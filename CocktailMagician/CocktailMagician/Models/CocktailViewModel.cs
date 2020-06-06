using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Web.Models
{
    public class CocktailViewModel
    {
        public CocktailViewModel()
        {
            this.Ingredients = new List<string>();
            AverageRating = 0.00;
        }
        public int Id { get; set; }

        [DisplayName("Cocktail Name")]
        [Required]
        [StringLength(40, ErrorMessage = "The Name cannot exceed 40 characters.")]
        public string Name { get; set; }

        [DisplayName("Cocktail Short Description")]
        [Required]
        [StringLength(350, ErrorMessage = "The short description cannot exceed 350 characters.")]
        public string ShortDescription { get; set; }

        [DisplayName("Cocktail Long Description")]
        [StringLength(3500, ErrorMessage = "The long description cannot exceed 3500 characters.")]
        public string LongDescription { get; set; }
        public string ImageUrl { get; set; }
        public string ImageThumbnailUrl { get; set; }
        public double? AverageRating { get; set; }
        public double SelectedRating { get; set; }
        public ICollection<string> Ingredients { get; set; }
        public string CurrentComment { get; set; }
        public ICollection<CocktailCommentViewModel> Comments { get; set; }

        public List<CheckBoxItem> SelectedIngredients { get; set; }
        public ICollection<IngredientViewModel> IngredientsChange { get; set; }

    }
}
