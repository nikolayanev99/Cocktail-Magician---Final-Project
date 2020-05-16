using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CocktailMagician.Models.Seeder
{
    public static class ModelBuilderExtention
    {
        public static void Seeder(this ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "bar crawler",
                    NormalizedName = "BAR CRAWLER",
                },
                new Role
                {
                    Id = 2,
                    Name = "cocktail magician",
                    NormalizedName = "COCKTAIL MAGICIAN",
                }
                );

            var passHasher = new PasswordHasher<User>();

            User admin = new User
            {
                Id = 1,
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",

            };

            admin.PasswordHash = passHasher.HashPassword(admin, "123321abv@BG");

            builder.Entity<User>().HasData(admin);

            builder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>
                {
                    RoleId = 2,
                    UserId = 1,
                }
                );



        }
    }
}
