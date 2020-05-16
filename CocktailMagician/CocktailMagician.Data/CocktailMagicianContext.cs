using System;
using CocktailMagician.Models;
using CocktailMagician.Models.Seeder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CocktailMagician.Data
{
    public class CocktailMagicianContext : IdentityDbContext<User, Role, int>

    {
        public CocktailMagicianContext(DbContextOptions<CocktailMagicianContext> options)
              : base(options)
        {

        }

        public DbSet<Bar> Bars { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Seeder();
            base.OnModelCreating(builder);
        }
    }
}
