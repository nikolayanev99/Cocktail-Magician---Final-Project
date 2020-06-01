using System;
using System.Collections.Generic;
using System.Text;
using CocktailMagician.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailMagician.Data.Configurations
{
    public class BarConfiguration : IEntityTypeConfiguration<Bar>
    {
        public void Configure(EntityTypeBuilder<Bar> builder)
        {
            builder.HasMany(i => i.BarCocktails)
                .WithOne(ii => ii.Bar)
                .HasForeignKey(ii => ii.BarId);
        }
    }
}

