using System;
using System.Collections.Generic;
using System.Text;
using CocktailMagician.Models;

namespace CocktailMagician.Services.DtoEntities
{
    public class BarDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Info { get; set; }

        public string Address { get; set; }

        public string PhotoPath { get; set; }

        public double AverageRating { get; set; }


        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
