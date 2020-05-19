using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace CocktailMagician.Models
{
    public class User : IdentityUser<int>
    {
        public User()
        {
            this.CocktailRatings = new List<CocktailRating>();
            this.CocktailComments = new List<CocktailComment>();
        }
        public ICollection<CocktailComment> CocktailComments { get; set; }
        public ICollection<CocktailRating> CocktailRatings { get; set; }
    }
}
