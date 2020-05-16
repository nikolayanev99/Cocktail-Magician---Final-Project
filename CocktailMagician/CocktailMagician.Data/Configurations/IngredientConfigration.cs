using CocktailMagician.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Data.Configurations
{
    public class IngredientConfigration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(n => n.Name)
                .IsRequired();

            builder.HasMany(i => i.CocktailIngredients)
                .WithOne(ii => ii.Ingredient)
                .HasForeignKey(ii => ii.IngredientId);
        }
    }
}
