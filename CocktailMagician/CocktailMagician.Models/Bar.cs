using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CocktailMagician.Models.Abstract;

namespace CocktailMagician.Models
{
    public class Bar : Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Bar Name")]
        [MaxLength(40, ErrorMessage = "The Name cannot exceed 40 characters.")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Bar Information")]
        [MaxLength(500, ErrorMessage = "The Description cannot exceed 500 characters")]
        public string Info { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "The Address cannot exceed 100 characters")]
        public string Address { get; set; }

        public string PhotoPath { get; set; }

        public ICollection<BarComment> Comments { get; set; }

        public ICollection<BarRating> Ratings { get; set; }


    }
}
