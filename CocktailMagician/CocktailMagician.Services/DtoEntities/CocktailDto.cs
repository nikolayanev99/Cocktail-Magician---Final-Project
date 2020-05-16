using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Services.DtoEntities
{
    public class CocktailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string ImageUrl { get; set; }
        public string ImageThumbnailUrl { get; set; }
        public double AverageRating { get; set; }
        public ICollection<string> Ingredients { get; set; }
    }
}
