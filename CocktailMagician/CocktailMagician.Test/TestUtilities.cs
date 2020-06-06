using System;
using System.Collections.Generic;
using System.Text;
using CocktailMagician.Data;
using Microsoft.EntityFrameworkCore;

namespace CocktailMagician.Test
{
    public class TestUtilities
    {
        public static DbContextOptions<CocktailMagicianContext> GetOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<CocktailMagicianContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }
    }
}
