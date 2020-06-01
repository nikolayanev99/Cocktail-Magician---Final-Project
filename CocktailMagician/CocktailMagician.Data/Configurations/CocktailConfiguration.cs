using CocktailMagician.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Data.Configurations
{
    public class CocktailConfiguration : IEntityTypeConfiguration<Cocktail>
    {
        public void Configure(EntityTypeBuilder<Cocktail> builder)
        {
            builder.HasKey(k => k.Id);
            
            builder.Property(n => n.Name)
                .IsRequired();

            builder.Property(d => d.ShortDescription)
                .IsRequired();

            builder.Property(i => i.ImageUrl);

            builder.HasMany(i => i.CocktailIngredients)
                .WithOne(c => c.Cocktail)
                .HasForeignKey(co => co.CocktailId);

            builder.HasMany(r => r.CocktailRatings)
                .WithOne(rr => rr.Cocktail);
            builder.HasMany(r => r.CocktailComments)
                .WithOne(rr => rr.Cocktail);

            builder.HasMany(i => i.BarCocktails)
                .WithOne(ii => ii.Cocktail)
                .HasForeignKey(ii => ii.CocktailId);
        }
    }
}
