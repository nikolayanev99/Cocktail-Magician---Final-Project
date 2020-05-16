using CocktailMagician.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CocktailMagician.Models
{
    public class Cocktail : Entity
    {
        public Cocktail()
        {
            this.CocktailIngredients = new List<CocktailIngredient>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [DisplayName ("Cocktail Name")]
        [Required]
        [StringLength(40, ErrorMessage = "The Name cannot exceed 40 characters.")]
        public string Name { get; set; }

        [DisplayName ("Cocktail Short Description")]
        [Required]
        [StringLength(100, ErrorMessage = "The short description cannot exceed 100 characters.")]
        public string ShortDescription { get; set; }

        [DisplayName ("Cocktail Long Description")]
        public string LongDescription { get; set; }
        public string ImageUrl { get; set; }
        public string ImageThumbnailUrl { get; set; }

        //public ICollection<CocktailComment> CocktailComments { get; set; }
        //public ICollection<CocktailRating> CocktailRatings { get; set; }
        public ICollection<CocktailIngredient> CocktailIngredients { get; set; }
        //public ICollection<BarCocktail> BarCocktails { get; set; }
    }
}
