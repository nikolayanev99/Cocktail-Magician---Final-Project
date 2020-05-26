using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using CocktailMagician.Models.Abstract;

namespace CocktailMagician.Models
{
    public class BarRating:Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Rating Value")]
        [Range(1,5, ErrorMessage ="Please enter a value between 1 and 5")]
        public int Value { get; set; }

        public int BarId { get; set; }

        public Bar Bar { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
