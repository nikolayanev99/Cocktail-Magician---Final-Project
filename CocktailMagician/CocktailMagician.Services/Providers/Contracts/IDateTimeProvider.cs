using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Services.Providers.Contracts
{
    public interface IDateTimeProvider
    {
        DateTime GetDateTime();
    }
}
