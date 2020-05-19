using CocktailMagician.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CocktailMagician.Models
{
    public class CocktailComment : Entity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "The text cannot exceed 500 characters.")]
        public string commentText { get; set; }
        public int CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
