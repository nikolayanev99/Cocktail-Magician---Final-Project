using CocktailMagician.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CocktailMagician.Models
{
    public class Ingredient : Entity
    {
        public Ingredient()
        {
            this.CocktailIngredients = new List<CocktailIngredient>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        
        [DisplayName("Ingredient Name")]
        [Required]
        [StringLength(20, ErrorMessage = "The Name of ingredient cannot exceed 20 characters")]
        public string Name { get; set; }
        public ICollection<CocktailIngredient> CocktailIngredients { get; set; }

    }
}
