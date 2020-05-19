using CocktailMagician.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Data.Configurations
{
    public class CocktailCommentConfiguration : IEntityTypeConfiguration<CocktailComment>
    {
        public void Configure(EntityTypeBuilder<CocktailComment> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(c => c.commentText)
                .IsRequired();

            builder.HasOne(u => u.User)
                .WithMany(uu => uu.CocktailComments);

            builder.HasOne(c => c.Cocktail)
                .WithMany(cc => cc.CocktailComments);
        }
    }
}
