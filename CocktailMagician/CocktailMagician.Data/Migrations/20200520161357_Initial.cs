using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CocktailMagician.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bars",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    Info = table.Column<string>(maxLength: 500, nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: false),
                    PhotoPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cocktails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    ShortDescription = table.Column<string>(maxLength: 350, nullable: false),
                    LongDescription = table.Column<string>(maxLength: 3500, nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    ImageThumbnailUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cocktails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 35, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CocktailComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    commentText = table.Column<string>(maxLength: 500, nullable: false),
                    CocktailId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CocktailComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CocktailComments_Cocktails_CocktailId",
                        column: x => x.CocktailId,
                        principalTable: "Cocktails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CocktailComments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CocktailRatings",
                columns: table => new
                {
                    CocktailId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CocktailRatings", x => new { x.CocktailId, x.UserId });
                    table.ForeignKey(
                        name: "FK_CocktailRatings_Cocktails_CocktailId",
                        column: x => x.CocktailId,
                        principalTable: "Cocktails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CocktailRatings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CocktailIngredients",
                columns: table => new
                {
                    CocktailId = table.Column<int>(nullable: false),
                    IngredientId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CocktailIngredients", x => new { x.CocktailId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_CocktailIngredients_Cocktails_CocktailId",
                        column: x => x.CocktailId,
                        principalTable: "Cocktails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CocktailIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "cd7a4067-2b84-45d1-8b91-38f3f699bfaa", "bar crawler", "BAR CRAWLER" },
                    { 2, "5fee4703-4b63-4577-b547-207e7fc4e644", "cocktail magician", "COCKTAIL MAGICIAN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, "aad0449a-e012-4936-992e-72095c6b6da0", "admin@admin.com", false, false, null, "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAEEFZFkYQggkvPInIYvikzSMDdajdItXQsOz9IKXzghlIXFqOw8fMTo4vqR5qrfHTEA==", null, false, null, false, "Admin" });

            migrationBuilder.InsertData(
                table: "Cocktails",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "ImageThumbnailUrl", "ImageUrl", "IsDeleted", "LongDescription", "ModifiedOn", "Name", "ShortDescription" },
                values: new object[,]
                {
                    { 10, new DateTime(2020, 5, 20, 16, 13, 56, 751, DateTimeKind.Utc).AddTicks(985), null, "", "/storage/images/cocktails/Dry-Martini-Cocktail.jpg", false, "The Dry martini cocktail is probably the most well-known cocktail of all time. James Bond may have had something to do with its modern-day resurgence but it has been popular since it first hit the market in the late 1800s. Today, the classic Martini is a sign of sophistication and martini drinks are often the most particular.  Perhaps for that reason, the Martini intimidates new bartenders. There are so many different variations and it has a lot consuming terminology surrounding it that make it seem far more complex than what it actually is. To make things more confusing, in the 90’s, bartenders started calling any cocktail that was served in a martini glass a ‘Martini.’ To clarify, most of these cocktails aren’t ‘real’ martinis. Most of them are, in fact, a type of sour cocktail, and have nothing to do with the classic recipe. It’s usually fairly obvious when someone wants a ‘new-age’ martini like a lychee martini or an espresso martini because that’s exactly what they’ll ask for. But for the classic martini order, you’ll need to get further clarification because people have different preferences and like I mentioned earlier, martini drinkers can be particular.", null, "Martini", "The first Martini, was it in California" },
                    { 9, new DateTime(2020, 5, 20, 16, 13, 56, 751, DateTimeKind.Utc).AddTicks(981), null, "", "/storage/images/cocktails/Manhattan-Cocktail.jpg", false, "Originating in the late 1800s, the Manhattan is the grandfather to the infamous Martini cocktail and it’s one classics that every bartender should know. Legend has it a bartender at the Manhattan club created this drink when Jennie Churchill (mother of Winston Churchill) threw a party for her father’s friend, the newly elected governor of New York, Samuel James Tilden. In his book ‘The Joy of Mixology,’ Gary Regan describes it as “Quite simply, when properly constructed, it is the finest cocktail on the face of the earth.” From a bartender as influential and skilled as Regan, that’s quite a statement! As a non-Manhattan drinker, I disagree, but that doesn’t change how important it is to know how to make this cocktail well. Variations : If possible, you should always offer your customer their choice of whiskey – the brand and either bourbon or rye. Feel free to experiment with different brands until you find something you love serving. This is also a great drink to experiment with different types of bitters. Start with peychauds & orange bitters. It can also be made in a rocks / old-fashioned glass served over ice if your customer prefers. A Dry Manhattan is made with dry vermouth instead of sweet vermouth using the same proportions (i.e. 1 shot) and a lemon twist instead of the cherry for the garnish. A Perfect Manhattan is made with half dry & half sweet vermouth (1/2 shot of each), and both a lemon twist & cherry for the garnish.", null, "Manhattan", "Of all the whiskey-based classic cocktails, the Manhattan is easily the best. " },
                    { 8, new DateTime(2020, 5, 20, 16, 13, 56, 751, DateTimeKind.Utc).AddTicks(978), null, "", "/storage/images/cocktails/Old-fashioned-cocktail.jpg", false, "Yet for all of its suave simplicity, the drink remains as relevant today as it was when it first captured drinkers’ hearts 200 years ago. In truth, you could draw a straight line connecting this drink to the first recorded definition of the cocktail category in general (circa 1806), which calls for spirits, sugar, water and bitters. You could also skip the history lesson and simply make the drink. Do the latter. Start by using good bourbon, the rule being that if you wouldn’t sip it by itself it has no place at the helm of a Bourbon Old Fashioned. (There are other whiskey drinks for masking subpar booze—this isn’t one of them.) From there, the cocktail-minded seem to break into two camps: simple syrup or muddled sugar. While a barspoon of syrup can cut your prep time in half, it robs the drink of some of the weight and texture that makes it so appealing. And anyway, what’s the big rush? The Bourbon Old Fashioned isn’t going anywhere.", null, "Old Fashioned", "When you get right down to it, the Bourbon Old Fashioned is little more than a slug of whiskey, seasoned and sweetened" },
                    { 1, new DateTime(2020, 5, 20, 16, 13, 56, 750, DateTimeKind.Utc).AddTicks(3106), null, "", "/storage/images/cocktails/Margarita-cocktail.jpg", false, "A margarita is a cocktail consisting of tequila, orange liqueur, and lime juice often served with salt on the rim of the glass. The drink is served shaken with ice (on the rocks), blended with ice (frozen margarita), or without ice (straight up). Although it has become acceptable to serve a margarita in a wide variety of glass types, ranging from cocktail and wine glasses to pint glasses and even large schooners, the drink is traditionally served in the eponymous margarita glass, a stepped-diameter variant of a cocktail glass or champagne coupe.", null, "Margarita", "The margarita is a popular Mexican and American drink" },
                    { 6, new DateTime(2020, 5, 20, 16, 13, 56, 751, DateTimeKind.Utc).AddTicks(971), null, "", "/storage/images/cocktails/Bloody-Mary-Cocktail.jpg", false, "Unlike most cocktails, the origins of the Bloody Mary cocktail are well known. It was created by bartender Fernand “Pete” Petiot at Harry’s New York bar in Paris around 1924 just after France started importing tinned tomatoes from America.In 1934, Petiot was brought to New York to head the bar at the St Regis Hotel during the prohibition era and he brought his creation with him. Americans have been drinking Bloody Mary’s ever since.When it was first created, it wasn’t very well liked. David Embury went as far as to describe the cocktail as “strictly vile” in his book ‘The Fine Art of Mixing Drinks.’ Today, many people still consider the Bloody Mary strictly vile, but it also has a loyal following of diehard fans that absolutely love it.Every bartender thinks they know ‘the best’ Bloody Mary recipe, but the truth, there is no 1 recipe because people’s preferences vary so widely. Some drinkers like it extra spicy, some prefer it mild. As such, you should always ask your guest how spicy they would like it on a scale of 1-10 and adjust the hot sauce you add appropriately.Despite it being individualized, there are a few general consensuses on how it should be made. Always use lemon juice over lime juice as it marries better with tomato juice. And it should always be rolled as opposed to shaking or stirring. Shaking emulsifies the tomato juice (a fancy way of saying, separating it and making it bubble), and stirring isn’t strong enough to adequately mix the ingredients.", null, "Bloody Mary", "The Bloody Mary was invented in the 1920s or 1930s." },
                    { 5, new DateTime(2020, 5, 20, 16, 13, 56, 751, DateTimeKind.Utc).AddTicks(967), null, "", "/storage/images/cocktails/Screwdriver-cocktail.jpg", false, "While the basic drink is simply the two ingredients, there are many variations. Many of the variations have different names in different parts of the world. The screwdriver is mentioned in 1944: A Screwdriver—a drink compounded of vodka and orange juice and supposedly invented by interned American fliers; the latest Yankee concoction of vodka and orange juice, called a 'screwdriver'.", null, "Screwdriver", "A screwdriver is a popular alcoholic highball drink made with orange juice and vodka." },
                    { 4, new DateTime(2020, 5, 20, 16, 13, 56, 751, DateTimeKind.Utc).AddTicks(964), null, "", "/storage/images/cocktails/Caipirinha-cocktail.jpg", false, "According to historians, the caipirinha was invented by landowning farmers in the region of Piracicaba, interior of the State of São Paulo during the 19th century as a local drink for 'high standard' events and parties, a reflection of the strong sugarcane culture in the region.", null, "Caipirinha", "Caipirinha is Brazil's national cocktail, made with cachaça (sugarcane hard liquor), sugar, and lime." },
                    { 2, new DateTime(2020, 5, 20, 16, 13, 56, 751, DateTimeKind.Utc).AddTicks(800), null, "", "/storage/images/cocktails/Mai-Tai-Cocktail.jpg", false, "The Mai Tai cocktail was created by bartender  “Trader” Vic Bergeron. In 1970, “Trader Vic” Bergeron wrote the following: “I originated the Mai Tai and put together a bit of the background of the evolution of this drink…. In 1944, after success with several exotic rum drinks, I felt a new drink was needed. I thought about all the really successful drinks: martinis, manhattans, daiquiris… All basically simple drinks…. I took down a bottle of 17 year old rum. It was J. Wray Nephew from Jamaica; surprisingly golden in colour, medium bodied, but with rich pungent flavour particular to Jamaican blends…. I took a fresh lime, added some orange curaçao from Holland, a dash of Rock Candy Syrup, and a dollop of French Orgeat, for its subtle almond flavour. A generous amount of shaved ice and vigorous shaking by hand produced the marriage I was after. Half the lime shell went in for color…. I stuck in a branch of fresh mint and gave two of them to  Ham and Carrie Guide, friends from Tahiti, who were there that night. Carrie took one sip and said, “Mai Tai— Roa Ai.” In Tahitian this means “Out of This World — The Best.” Well, that was that. I named the drink “Mai Tai.” … In fairness to myself and to a truly great drink, I hope you will agree when I say, “let’s get the record straight on the Mai Tai.” The ingredients “Trader Vic” originally used are hard to come by today. So the above recipe is an excellent recipe that’s more accessible.", null, "Mai Tai", "A modern classic and arguably the most popular tiki drink on the planet." },
                    { 3, new DateTime(2020, 5, 20, 16, 13, 56, 751, DateTimeKind.Utc).AddTicks(958), null, "", "/storage/images/cocktails/White-Russian-Cocktail.jpg", false, "The traditional cocktail known as a Black Russian, which first appeared in 1949, becomes a White Russian with the addition of cream. Neither drink is Russian in origin, but both are so named due to vodka being the primary ingredient. It is unclear which drink preceded the other.", null, "White Russian", " It’s simply a black Russian with the addition of cream." },
                    { 7, new DateTime(2020, 5, 20, 16, 13, 56, 751, DateTimeKind.Utc).AddTicks(974), null, "", "/storage/images/cocktails/Whiskey-Sour-Cocktail.jpg", false, "The whiskey sour is a mixed drink containing whiskey (often bourbon), lemon juice, sugar, and optionally, a dash of egg white. With the egg white, it is sometimes called a Boston Sour. With a few bar spoons of full-bodied red wine floated on top, it is often referred to as a New York Sour. It is shaken and served either straight up or over ice. The traditional garnish is half an orange slice and a maraschino cherry. A variant of the whiskey sour is the Ward 8, which often is based on bourbon or rye whiskey, and includes both lemon and orange juices, and grenadine syrup as the sweetener. The egg white sometimes employed in other whiskey sours is not usually included.", null, "Whiskey Sour", "Spirit, sugar, citrus—the original big three—come together in the Whiskey Sour" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 19, new DateTime(2020, 5, 20, 16, 13, 56, 750, DateTimeKind.Utc).AddTicks(1016), null, false, null, "Pink Grapefruit Juice" },
                    { 20, new DateTime(2020, 5, 20, 16, 13, 56, 750, DateTimeKind.Utc).AddTicks(1018), null, false, null, "Cointreau" },
                    { 21, new DateTime(2020, 5, 20, 16, 13, 56, 750, DateTimeKind.Utc).AddTicks(1020), null, false, null, "Soda" },
                    { 22, new DateTime(2020, 5, 20, 16, 13, 56, 750, DateTimeKind.Utc).AddTicks(1022), null, false, null, "Cherry Brandy" },
                    { 23, new DateTime(2020, 5, 20, 16, 13, 56, 750, DateTimeKind.Utc).AddTicks(1024), null, false, null, "Lemon" },
                    { 25, new DateTime(2020, 5, 20, 16, 13, 56, 750, DateTimeKind.Utc).AddTicks(1028), null, false, null, "Pineapple Juice" },
                    { 26, new DateTime(2020, 5, 20, 16, 13, 56, 750, DateTimeKind.Utc).AddTicks(1031), null, false, null, "Coconut Cream" },
                    { 27, new DateTime(2020, 5, 20, 16, 13, 56, 750, DateTimeKind.Utc).AddTicks(1033), null, false, null, "Grapefruit Juice" },
                    { 28, new DateTime(2020, 5, 20, 16, 13, 56, 750, DateTimeKind.Utc).AddTicks(1035), null, false, null, "Cola" },
                    { 29, new DateTime(2020, 5, 20, 16, 13, 56, 750, DateTimeKind.Utc).AddTicks(1037), null, false, null, "Peach" },
                    { 30, new DateTime(2020, 5, 20, 16, 13, 56, 750, DateTimeKind.Utc).AddTicks(1039), null, false, null, "Prosecco" },
                    { 24, new DateTime(2020, 5, 20, 16, 13, 56, 750, DateTimeKind.Utc).AddTicks(1026), null, false, null, "Sloe Gin" },
                    { 18, new DateTime(2020, 5, 20, 16, 13, 56, 750, DateTimeKind.Utc).AddTicks(1014), null, false, null, "White Rum" },
                    { 15, new DateTime(2020, 5, 20, 16, 13, 56, 750, DateTimeKind.Utc).AddTicks(1007), null, false, null, "Italian Sweet Vermouth" },
                    { 16, new DateTime(2020, 5, 20, 16, 13, 56, 750, DateTimeKind.Utc).AddTicks(1009), null, false, null, "French Dry Vermouth" },
                    { 14, new DateTime(2020, 5, 20, 16, 13, 56, 750, DateTimeKind.Utc).AddTicks(1005), null, false, null, "Water" },
                    { 13, new DateTime(2020, 5, 20, 16, 13, 56, 750, DateTimeKind.Utc).AddTicks(1003), null, false, null, "Bourbon" },
                    { 11, new DateTime(2020, 5, 20, 16, 13, 56, 750, DateTimeKind.Utc).AddTicks(999), null, false, null, "Orange Juice" },
                    { 10, new DateTime(2020, 5, 20, 16, 13, 56, 750, DateTimeKind.Utc).AddTicks(997), null, false, null, "Cachaca" },
                    { 9, new DateTime(2020, 5, 20, 16, 13, 56, 750, DateTimeKind.Utc).AddTicks(995), null, false, null, "Vodka" },
                    { 8, new DateTime(2020, 5, 20, 16, 13, 56, 750, DateTimeKind.Utc).AddTicks(993), null, false, null, "Kahlua" },
                    { 7, new DateTime(2020, 5, 20, 16, 13, 56, 750, DateTimeKind.Utc).AddTicks(990), null, false, null, "Fresh Cream" },
                    { 6, new DateTime(2020, 5, 20, 16, 13, 56, 750, DateTimeKind.Utc).AddTicks(988), null, false, null, "Almond Surup" },
                    { 5, new DateTime(2020, 5, 20, 16, 13, 56, 750, DateTimeKind.Utc).AddTicks(986), null, false, null, "Orange Curacao" },
                    { 4, new DateTime(2020, 5, 20, 16, 13, 56, 750, DateTimeKind.Utc).AddTicks(984), null, false, null, "Drak Rum" },
                    { 3, new DateTime(2020, 5, 20, 16, 13, 56, 750, DateTimeKind.Utc).AddTicks(979), null, false, null, "Tequila" },
                    { 2, new DateTime(2020, 5, 20, 16, 13, 56, 750, DateTimeKind.Utc).AddTicks(932), null, false, null, "Triple Sec" },
                    { 1, new DateTime(2020, 5, 20, 16, 13, 56, 749, DateTimeKind.Utc).AddTicks(7954), null, false, null, "Lime Juice" },
                    { 17, new DateTime(2020, 5, 20, 16, 13, 56, 750, DateTimeKind.Utc).AddTicks(1012), null, false, null, "Gin" },
                    { 12, new DateTime(2020, 5, 20, 16, 13, 56, 750, DateTimeKind.Utc).AddTicks(1002), null, false, null, "Tomato Juice" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "CocktailIngredients",
                columns: new[] { "CocktailId", "IngredientId", "DeletedOn", "IsDeleted" },
                values: new object[,]
                {
                    { 10, 16, null, false },
                    { 9, 15, null, false },
                    { 8, 14, null, false },
                    { 9, 13, null, false },
                    { 8, 13, null, false },
                    { 7, 13, null, false },
                    { 6, 12, null, false },
                    { 5, 11, null, false },
                    { 4, 10, null, false },
                    { 6, 9, null, false },
                    { 5, 9, null, false },
                    { 3, 9, null, false },
                    { 3, 8, null, false },
                    { 3, 7, null, false },
                    { 2, 6, null, false },
                    { 2, 5, null, false },
                    { 2, 4, null, false },
                    { 1, 3, null, false },
                    { 1, 2, null, false },
                    { 7, 1, null, false },
                    { 2, 1, null, false },
                    { 10, 17, null, false },
                    { 1, 1, null, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CocktailComments_CocktailId",
                table: "CocktailComments",
                column: "CocktailId");

            migrationBuilder.CreateIndex(
                name: "IX_CocktailComments_UserId",
                table: "CocktailComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CocktailIngredients_IngredientId",
                table: "CocktailIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_CocktailRatings_UserId",
                table: "CocktailRatings",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Bars");

            migrationBuilder.DropTable(
                name: "CocktailComments");

            migrationBuilder.DropTable(
                name: "CocktailIngredients");

            migrationBuilder.DropTable(
                name: "CocktailRatings");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Cocktails");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
