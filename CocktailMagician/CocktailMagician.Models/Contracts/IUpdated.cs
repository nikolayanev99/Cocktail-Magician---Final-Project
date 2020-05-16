using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Models.Contracts
{
    public interface IUpdated
    {
        DateTime CreatedOn { get; set; }
        DateTime? ModifiedOn { get; set; }
    }
}
