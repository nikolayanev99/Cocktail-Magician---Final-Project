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

            // Ingredients

            //     ||
            //     ||
            //   ()()()
            //    ()()
            //     ()
            var limeJuice = new Ingredient
            {
                Id = 1,
                Name = "Lime Juice"
            };
            var tripleSec = new Ingredient
            {
                Id = 2,
                Name = "Triple Sec"
            };
            var tequila = new Ingredient
            {
                Id = 3,
                Name = "Tequila"
            };
            var darkRum = new Ingredient
            {
                Id = 4,
                Name = "Drak Rum"
            };
            var orangeCuracao = new Ingredient
            {
                Id = 5,
                Name = "Orange Curacao"
            };
            var almondSurup = new Ingredient
            {
                Id = 6,
                Name = "Almond Surup"
            };
            var freshCream = new Ingredient
            {
                Id = 7,
                Name = "Fresh Cream"
            };
            var kahlua = new Ingredient
            {
                Id = 8,
                Name = "Kahlua"
            };
            var vodka = new Ingredient
            {
                Id = 9,
                Name = "Vodka"
            };
            var cachaca = new Ingredient
            {
                Id = 10,
                Name = "Cachaca"
            };
            var orangeJuice = new Ingredient
            {
                Id = 11,
                Name = "Orange Juice"
            };
            var tomatoJuice = new Ingredient
            {
                Id = 12,
                Name = "Tomato Juice"
            };
            var bourbon = new Ingredient
            {
                Id = 13,
                Name = "Bourbon"
            };
            var water = new Ingredient
            {
                Id = 14,
                Name = "Water"
            };
            var italianSweetVermouth = new Ingredient
            {
                Id = 15,
                Name = "Italian Sweet Vermouth"
            };
            var frenchDryVermouth = new Ingredient
            {
                Id = 16,
                Name = "French Dry Vermouth"
            };
            var gin = new Ingredient
            {
                Id = 17,
                Name = "Gin"
            };
            var whiteRum = new Ingredient
            {
                Id = 18,
                Name = "White Rum"
            };
            var pinkGrapefruitJuice = new Ingredient
            {
                Id = 19,
                Name = "Pink Grapefruit Juice"
            };
            var cointreau = new Ingredient
            {
                Id = 20,
                Name = "Cointreau"
            };
            var soda = new Ingredient
            {
                Id = 21,
                Name = "Soda"
            };
            var cherryBrandy = new Ingredient
            {
                Id = 22,
                Name = "Cherry Brandy"
            };
            var lemon = new Ingredient
            {
                Id = 23,
                Name = "Lemon"
            };
            var sloeGin = new Ingredient
            {
                Id = 24,
                Name = "Sloe Gin"
            };
            var pineappleJuice = new Ingredient
            {
                Id = 25,
                Name = "Pineapple Juice"
            };
            var coconutCream = new Ingredient
            {
                Id = 26,
                Name = "Coconut Cream"
            };
            var grapefruitJuice = new Ingredient
            {
                Id = 27,
                Name = "Grapefruit Juice"
            };
            var cola = new Ingredient
            {
                Id = 28,
                Name = "Cola"
            };
            var peach = new Ingredient
            {
                Id = 29,
                Name = "Peach"
            };
            var prosecco = new Ingredient
            {
                Id = 30,
                Name = "Prosecco"
            };

            builder.Entity<Ingredient>().HasData(limeJuice, tripleSec, tequila, darkRum, orangeCuracao, almondSurup, freshCream, kahlua, vodka, cachaca, orangeJuice, tomatoJuice, bourbon, water, italianSweetVermouth, frenchDryVermouth, gin, whiteRum, pinkGrapefruitJuice, cointreau, soda, cherryBrandy, lemon, sloeGin, pineappleJuice, coconutCream, grapefruitJuice, cola, peach, prosecco);

            // Cocktails

            //     ||
            //     ||
            //   ()()()
            //    ()()
            //     ()

            var margarita = new Cocktail
            {
                Id = 1,
                Name = "Margarita",
                ShortDescription = "The margarita is a popular Mexican and American drink",
                LongDescription = "A margarita is a cocktail consisting of tequila, orange liqueur, and lime juice often served with salt on the rim of the glass. The drink is served shaken with ice (on the rocks), blended with ice (frozen margarita), or without ice (straight up). Although it has become acceptable to serve a margarita in a wide variety of glass types, ranging from cocktail and wine glasses to pint glasses and even large schooners, the drink is traditionally served in the eponymous margarita glass, a stepped-diameter variant of a cocktail glass or champagne coupe.",
                ImageUrl = "/storage/images/cocktails/Margarita-cocktail.jpg",
                ImageThumbnailUrl = ""
            };
            var maiTai = new Cocktail
            {
                Id = 2,
                Name = "Mai Tai",
                ShortDescription = "A modern classic and arguably the most popular tiki drink on the planet.",
                LongDescription = "The Mai Tai cocktail was created by bartender  “Trader” Vic Bergeron. In 1970, “Trader Vic” Bergeron wrote the following: “I originated the Mai Tai and put together a bit of the background of the evolution of this drink…. In 1944, after success with several exotic rum drinks, I felt a new drink was needed. I thought about all the really successful drinks: martinis, manhattans, daiquiris… All basically simple drinks…. I took down a bottle of 17 year old rum. It was J. Wray Nephew from Jamaica; surprisingly golden in colour, medium bodied, but with rich pungent flavour particular to Jamaican blends…. I took a fresh lime, added some orange curaçao from Holland, a dash of Rock Candy Syrup, and a dollop of French Orgeat, for its subtle almond flavour. A generous amount of shaved ice and vigorous shaking by hand produced the marriage I was after. Half the lime shell went in for color…. I stuck in a branch of fresh mint and gave two of them to  Ham and Carrie Guide, friends from Tahiti, who were there that night. Carrie took one sip and said, “Mai Tai— Roa Ai.” In Tahitian this means “Out of This World — The Best.” Well, that was that. I named the drink “Mai Tai.” … In fairness to myself and to a truly great drink, I hope you will agree when I say, “let’s get the record straight on the Mai Tai.” The ingredients “Trader Vic” originally used are hard to come by today. So the above recipe is an excellent recipe that’s more accessible.",
                ImageUrl = "/storage/images/cocktails/Mai-Tai-Cocktail.jpg",
                ImageThumbnailUrl = ""
            };
            var whiteRussian = new Cocktail
            {
                Id = 3,
                Name = "White Russian",
                ShortDescription = " It’s simply a black Russian with the addition of cream.",
                LongDescription = "The traditional cocktail known as a Black Russian, which first appeared in 1949, becomes a White Russian with the addition of cream. Neither drink is Russian in origin, but both are so named due to vodka being the primary ingredient. It is unclear which drink preceded the other.",
                ImageUrl = "/storage/images/cocktails/White-Russian-Cocktail.jpg",
                ImageThumbnailUrl = ""
            };
            var caipirinha = new Cocktail
            {
                Id = 4,
                Name = "Caipirinha",
                ShortDescription = "Caipirinha is Brazil's national cocktail, made with cachaça (sugarcane hard liquor), sugar, and lime.",
                LongDescription = "According to historians, the caipirinha was invented by landowning farmers in the region of Piracicaba, interior of the State of São Paulo during the 19th century as a local drink for 'high standard' events and parties, a reflection of the strong sugarcane culture in the region.",
                ImageUrl = "/storage/images/cocktails/Caipirinha-cocktail.jpg",
                ImageThumbnailUrl = ""
            };
            var screwdriver = new Cocktail
            {
                Id = 5,
                Name = "Screwdriver",
                ShortDescription = "A screwdriver is a popular alcoholic highball drink made with orange juice and vodka.",
                LongDescription = "While the basic drink is simply the two ingredients, there are many variations. Many of the variations have different names in different parts of the world. The screwdriver is mentioned in 1944: A Screwdriver—a drink compounded of vodka and orange juice and supposedly invented by interned American fliers; the latest Yankee concoction of vodka and orange juice, called a 'screwdriver'.",
                ImageUrl = "/storage/images/cocktails/Screwdriver-cocktail.jpg",
                ImageThumbnailUrl = ""
            };
            var bloodyMary = new Cocktail
            {
                Id = 6,
                Name = "Bloody Mary",
                ShortDescription = "The Bloody Mary was invented in the 1920s or 1930s.",
                LongDescription = "Unlike most cocktails, the origins of the Bloody Mary cocktail are well known. It was created by bartender Fernand “Pete” Petiot at Harry’s New York bar in Paris around 1924 just after France started importing tinned tomatoes from America.In 1934, Petiot was brought to New York to head the bar at the St Regis Hotel during the prohibition era and he brought his creation with him. Americans have been drinking Bloody Mary’s ever since.When it was first created, it wasn’t very well liked. David Embury went as far as to describe the cocktail as “strictly vile” in his book ‘The Fine Art of Mixing Drinks.’ Today, many people still consider the Bloody Mary strictly vile, but it also has a loyal following of diehard fans that absolutely love it.Every bartender thinks they know ‘the best’ Bloody Mary recipe, but the truth, there is no 1 recipe because people’s preferences vary so widely. Some drinkers like it extra spicy, some prefer it mild. As such, you should always ask your guest how spicy they would like it on a scale of 1-10 and adjust the hot sauce you add appropriately.Despite it being individualized, there are a few general consensuses on how it should be made. Always use lemon juice over lime juice as it marries better with tomato juice. And it should always be rolled as opposed to shaking or stirring. Shaking emulsifies the tomato juice (a fancy way of saying, separating it and making it bubble), and stirring isn’t strong enough to adequately mix the ingredients.",
                ImageUrl = "/storage/images/cocktails/Bloody-Mary-Cocktail.jpg",
                ImageThumbnailUrl = ""
            };
            var whiskeySour = new Cocktail
            {
                Id = 7,
                Name = "Whiskey Sour",
                ShortDescription = "Spirit, sugar, citrus—the original big three—come together in the Whiskey Sour",
                LongDescription = "The whiskey sour is a mixed drink containing whiskey (often bourbon), lemon juice, sugar, and optionally, a dash of egg white. With the egg white, it is sometimes called a Boston Sour. With a few bar spoons of full-bodied red wine floated on top, it is often referred to as a New York Sour. It is shaken and served either straight up or over ice. The traditional garnish is half an orange slice and a maraschino cherry. A variant of the whiskey sour is the Ward 8, which often is based on bourbon or rye whiskey, and includes both lemon and orange juices, and grenadine syrup as the sweetener. The egg white sometimes employed in other whiskey sours is not usually included.",
                ImageUrl = "/storage/images/cocktails/Whiskey-Sour-Cocktail.jpg",
                ImageThumbnailUrl = ""
            };
            var oldFashioned = new Cocktail
            {
                Id = 8,
                Name = "Old Fashioned",
                ShortDescription = "When you get right down to it, the Bourbon Old Fashioned is little more than a slug of whiskey, seasoned and sweetened",
                LongDescription = "Yet for all of its suave simplicity, the drink remains as relevant today as it was when it first captured drinkers’ hearts 200 years ago. In truth, you could draw a straight line connecting this drink to the first recorded definition of the cocktail category in general (circa 1806), which calls for spirits, sugar, water and bitters. You could also skip the history lesson and simply make the drink. Do the latter. Start by using good bourbon, the rule being that if you wouldn’t sip it by itself it has no place at the helm of a Bourbon Old Fashioned. (There are other whiskey drinks for masking subpar booze—this isn’t one of them.) From there, the cocktail-minded seem to break into two camps: simple syrup or muddled sugar. While a barspoon of syrup can cut your prep time in half, it robs the drink of some of the weight and texture that makes it so appealing. And anyway, what’s the big rush? The Bourbon Old Fashioned isn’t going anywhere.",
                ImageUrl = "/storage/images/cocktails/Old-fashioned-cocktail.jpg",
                ImageThumbnailUrl = ""
            };
            var manhattan = new Cocktail
            {
                Id = 9,
                Name = "Manhattan",
                ShortDescription = "Of all the whiskey-based classic cocktails, the Manhattan is easily the best. ",
                LongDescription = "Originating in the late 1800s, the Manhattan is the grandfather to the infamous Martini cocktail and it’s one classics that every bartender should know. Legend has it a bartender at the Manhattan club created this drink when Jennie Churchill (mother of Winston Churchill) threw a party for her father’s friend, the newly elected governor of New York, Samuel James Tilden. In his book ‘The Joy of Mixology,’ Gary Regan describes it as “Quite simply, when properly constructed, it is the finest cocktail on the face of the earth.” From a bartender as influential and skilled as Regan, that’s quite a statement! As a non-Manhattan drinker, I disagree, but that doesn’t change how important it is to know how to make this cocktail well. Variations : If possible, you should always offer your customer their choice of whiskey – the brand and either bourbon or rye. Feel free to experiment with different brands until you find something you love serving. This is also a great drink to experiment with different types of bitters. Start with peychauds & orange bitters. It can also be made in a rocks / old-fashioned glass served over ice if your customer prefers. A Dry Manhattan is made with dry vermouth instead of sweet vermouth using the same proportions (i.e. 1 shot) and a lemon twist instead of the cherry for the garnish. A Perfect Manhattan is made with half dry & half sweet vermouth (1/2 shot of each), and both a lemon twist & cherry for the garnish.",
                ImageUrl = "/storage/images/cocktails/Manhattan-Cocktail.jpg",
                ImageThumbnailUrl = ""
            };
            var martini = new Cocktail
            {
                Id = 10,
                Name = "Martini",
                ShortDescription = "The first Martini, was it in California",
                LongDescription = "The Dry martini cocktail is probably the most well-known cocktail of all time. James Bond may have had something to do with its modern-day resurgence but it has been popular since it first hit the market in the late 1800s. Today, the classic Martini is a sign of sophistication and martini drinks are often the most particular.  Perhaps for that reason, the Martini intimidates new bartenders. There are so many different variations and it has a lot consuming terminology surrounding it that make it seem far more complex than what it actually is. To make things more confusing, in the 90’s, bartenders started calling any cocktail that was served in a martini glass a ‘Martini.’ To clarify, most of these cocktails aren’t ‘real’ martinis. Most of them are, in fact, a type of sour cocktail, and have nothing to do with the classic recipe. It’s usually fairly obvious when someone wants a ‘new-age’ martini like a lychee martini or an espresso martini because that’s exactly what they’ll ask for. But for the classic martini order, you’ll need to get further clarification because people have different preferences and like I mentioned earlier, martini drinkers can be particular.",
                ImageUrl = "/storage/images/cocktails/Dry-Martini-Cocktail.jpg",
                ImageThumbnailUrl = ""
            };

            builder.Entity<Cocktail>().HasData(margarita, maiTai, whiteRussian, caipirinha);
            
            // CocktailIngredients

            //     ||
            //     ||
            //   ()()()
            //    ()()
            //     ()
            var cocktailIngredientMargarita1 = new CocktailIngredient
            {
                CocktailId = margarita.Id,
                IngredientId = limeJuice.Id,
            };
            var cocktailIngredientMargarita2 = new CocktailIngredient
            {
                CocktailId = margarita.Id,
                IngredientId = tripleSec.Id,
            };
            var cocktailIngredientMargarita3 = new CocktailIngredient
            {
                CocktailId = maiTai.Id,
                IngredientId = darkRum.Id,
            };
            var cocktailIngredientMaiTai1 = new CocktailIngredient
            {
                CocktailId = maiTai.Id,
                IngredientId = orangeCuracao.Id,
            };
            var cocktailIngredientMaiTai2 = new CocktailIngredient
            {
                CocktailId = maiTai.Id,
                IngredientId = limeJuice.Id,
            };
            var cocktailIngredientMaiTai3 = new CocktailIngredient
            {
                CocktailId = maiTai.Id,
                IngredientId = almondSurup.Id,
            };
            var cocktailIngredientMaiTai4 = new CocktailIngredient
            {
                CocktailId = margarita.Id,
                IngredientId = tequila.Id,
            };
            var cocktailIngredientWhiteRussian1 = new CocktailIngredient
            {
                CocktailId = whiteRussian.Id,
                IngredientId = freshCream.Id,
            };
            var cocktailIngredientWhiteRussian2 = new CocktailIngredient
            {
                CocktailId = whiteRussian.Id,
                IngredientId = kahlua.Id,
            };
            var cocktailIngredientWhiteRussian3 = new CocktailIngredient
            {
                CocktailId = whiteRussian.Id,
                IngredientId = vodka.Id,
            };
            var cocktailIngredientCaipirinha = new CocktailIngredient
            {
                CocktailId = caipirinha.Id,
                IngredientId = cachaca.Id,
            };
            var cocktailIngredientScrewdriver1 = new CocktailIngredient
            {
                CocktailId = screwdriver.Id,
                IngredientId = orangeJuice.Id,
            };
            var cocktailIngredientScrewdriver2 = new CocktailIngredient
            {
                CocktailId = screwdriver.Id,
                IngredientId = vodka.Id,
            };
            var cocktailIngredientBloodyMary1 = new CocktailIngredient
            {
                CocktailId = bloodyMary.Id,
                IngredientId = tomatoJuice.Id,
            };
            var cocktailIngredientBloodyMary2 = new CocktailIngredient
            {
                CocktailId = bloodyMary.Id,
                IngredientId = vodka.Id,
            };
            var cocktailIngredientWhiskeySour1 = new CocktailIngredient
            {
                CocktailId = whiskeySour.Id,
                IngredientId = bourbon.Id,
            };
            var cocktailIngredientWhiskeySour2 = new CocktailIngredient
            {
                CocktailId = whiskeySour.Id,
                IngredientId = limeJuice.Id,
            };
            var cocktailIngredientOldFashioned1 = new CocktailIngredient
            {
                CocktailId = oldFashioned.Id,
                IngredientId = bourbon.Id,
            };
            var cocktailIngredientOldFashioned2 = new CocktailIngredient
            {
                CocktailId = oldFashioned.Id,
                IngredientId = water.Id,
            };
            var cocktailIngredientManhattan1 = new CocktailIngredient
            {
                CocktailId = manhattan.Id,
                IngredientId = italianSweetVermouth.Id,
            };
            var cocktailIngredientManhattan2 = new CocktailIngredient
            {
                CocktailId = manhattan.Id,
                IngredientId = bourbon.Id,
            };
            var cocktailIngredientMartini1 = new CocktailIngredient
            {
                CocktailId = martini.Id,
                IngredientId = frenchDryVermouth.Id,
            };
            var cocktailIngredientMartini2 = new CocktailIngredient
            {
                CocktailId = martini.Id,
                IngredientId = gin.Id,
            };

            builder.Entity<CocktailIngredient>().HasData(cocktailIngredientMargarita1, cocktailIngredientMargarita2, cocktailIngredientMargarita3, cocktailIngredientMaiTai1, cocktailIngredientMaiTai2, cocktailIngredientMaiTai3, cocktailIngredientMaiTai4, cocktailIngredientWhiteRussian1, cocktailIngredientWhiteRussian2, cocktailIngredientWhiteRussian3, cocktailIngredientCaipirinha, cocktailIngredientScrewdriver1, cocktailIngredientScrewdriver2, cocktailIngredientBloodyMary1, cocktailIngredientBloodyMary2, cocktailIngredientWhiskeySour1, cocktailIngredientWhiskeySour2, cocktailIngredientOldFashioned1, cocktailIngredientOldFashioned2, cocktailIngredientManhattan1, cocktailIngredientManhattan2, cocktailIngredientMartini1, cocktailIngredientMartini2);
        }
    }
}
