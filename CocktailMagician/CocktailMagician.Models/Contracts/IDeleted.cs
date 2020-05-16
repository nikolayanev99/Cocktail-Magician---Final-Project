using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Models.Contracts
{
    public interface IDeleted
    {
        bool IsDeleted { get; set; }
        DateTime? DeletedOn { get; set; }
    }
}
