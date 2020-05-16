﻿using System;
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
       
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CocktailConfigurations());
            builder.ApplyConfiguration(new IngredientConfigration());
            builder.ApplyConfiguration(new CocktailIngredientConfiguration());
            
            builder.Seeder();
            base.OnModelCreating(builder);
        }
    }
}
