using System;
using CocktailMagician.Data.Configurations;
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
        public DbSet<Cocktail> Cocktails { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<CocktailIngredient> CocktailIngredients { get; set; }
        public DbSet<CocktailRating> CocktailRatings { get; set; }
        public DbSet<CocktailComment> CocktailComments { get;set; }
        public DbSet<BarComment> BarComments { get; set; }
       
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CocktailConfiguration());
            builder.ApplyConfiguration(new IngredientConfigration());
            builder.ApplyConfiguration(new CocktailIngredientConfiguration());
<<<<<<< HEAD
=======
            builder.ApplyConfiguration(new CocktailCommentConfiguration());
>>>>>>> c816df27f39902a56ab2fabd89474b9d76fe582f
            builder.ApplyConfiguration(new CocktailRatingConfiguration());
            
            builder.Seeder();
            base.OnModelCreating(builder);
        }
    }
}
