using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Web.Models
{
    public class BarCommentViewModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Comment Text")]
        [MaxLength(500, ErrorMessage = "The Comment cannot exceed 500 characters.")]
        public string Text { get; set; }

        public int BarId { get; set; }

        public int UserId { get; set; }

        public string Author { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    }
}
