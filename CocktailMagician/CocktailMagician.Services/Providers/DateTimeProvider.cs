using System;
using System.Collections.Generic;
using System.Text;
using CocktailMagician.Services.Providers.Contracts;

namespace CocktailMagician.Services.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetDateTime() => DateTime.UtcNow;
    }
}
