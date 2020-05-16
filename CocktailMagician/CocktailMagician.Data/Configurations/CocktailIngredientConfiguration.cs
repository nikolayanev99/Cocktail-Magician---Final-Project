using CocktailMagician.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CocktailMagician.Data.Configurations
{
    public class CocktailIngredientConfiguration : IEntityTypeConfiguration<CocktailIngredient>
    {
        public void Configure(EntityTypeBuilder<CocktailIngredient> builder)
        {
            builder.HasKey(key => new { key.CocktailId, key.IngredientId });

            builder.HasOne(c => c.Cocktail)
                .WithMany(i => i.CocktailIngredients)
                .HasForeignKey(ii => ii.CocktailId);

            builder.HasOne(c => c.Ingredient)
                .WithMany(i => i.CocktailIngredients)
                .HasForeignKey(ii => ii.IngredientId);
        }
    }
}
