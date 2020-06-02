using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Models;
using Microsoft.AspNetCore.Http;

namespace CocktailMagician.Web.Models
{
    public class BarViewModel
    {
        public BarViewModel()
        {
            AverageRating = 0.00;
        }

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

        public IFormFile Photo { get; set; }

        public double AverageRating { get; set; }

        public string CurrentComment { get; set; }

        public double SelectedRating { get; set; }

        public ICollection<BarCommentViewModel> Comments { get; set; }

        public ICollection<string> Cocktails { get; set; }

        public ICollection<BarRatingViewModel> Ratings { get; set; }
    }
}
