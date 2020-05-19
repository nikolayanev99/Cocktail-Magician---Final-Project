using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Services.DtoEntities
{
    public class CocktailRatingDto
    {
        public double Value { get; set; }
        public int CocktailId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
