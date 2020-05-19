using CocktailMagician.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Data.Configurations
{
    public class CocktailRatingConfiguration : IEntityTypeConfiguration<CocktailRating>
    {
        public void Configure(EntityTypeBuilder<CocktailRating> builder)
        {
            builder.HasKey(k => new { k.CocktailId, k.UserId });
            builder.Property(v => v.Value)
                .IsRequired();

            builder.HasOne(u => u.User)
                .WithMany(r => r.CocktailRatings)
                .HasForeignKey(uu => uu.UserId);

            builder.HasOne(c => c.Cocktail)
                .WithMany(r => r.CocktailRatings)
                .HasForeignKey(cc => cc.CocktailId);
                    
        }
    }
}
