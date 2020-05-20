using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using CocktailMagician.Models.Abstract;

namespace CocktailMagician.Models
{
    public class BarComment : Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Comment Text")]
        [MaxLength(500, ErrorMessage = "The Comment cannot exceed 500 characters.")]
        public string Text { get; set; }

        public int BarId { get; set; }

        public Bar Bar { get; set; }

        public int UserId { get; set; }

        public User Author { get; set; }


    }
}
