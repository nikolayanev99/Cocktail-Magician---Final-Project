using CocktailMagician.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CocktailMagician.Models
{
    public class CocktailRating : Entity
    {

        [Required]
        [Range(1, 5, ErrorMessage = "Is not valid value")]
        public double Value { get; set; }
        public int CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
