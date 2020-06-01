using System;
using System.Collections.Generic;
using System.Text;
using CocktailMagician.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailMagician.Data.Configurations
{
    public class BarCocktailConfiguration : IEntityTypeConfiguration<BarCocktail>
    {
        public void Configure(EntityTypeBuilder<BarCocktail> builder)
        {
            builder.HasKey(key => new { key.CocktailId, key.BarId });

            builder.HasOne(bc => bc.Bar)
                .WithMany(u => u.BarCocktails)
                .HasForeignKey(u => u.BarId);

            builder.HasOne(bc => bc.Cocktail)
                .WithMany(b => b.BarCocktails)
                .HasForeignKey(b => b.CocktailId);
        }
    }
}
