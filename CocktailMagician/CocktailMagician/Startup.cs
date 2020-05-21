using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Data;
using CocktailMagician.Models;
using CocktailMagician.Services;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Services.DtoMappers;
using CocktailMagician.Services.DtoMappers.Contracts;
using CocktailMagician.Services.Providers;
using CocktailMagician.Services.Providers.Contracts;
using CocktailMagician.Web.Mappers;
using CocktailMagician.Web.Mappers.Contracts;
using CocktailMagician.Web.Models;
using CocktailMagician.Web.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CocktailMagician
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });

            services.AddRazorPages();
            services.AddIdentity<User, Role>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<CocktailMagicianContext>()                
                .AddDefaultTokenProviders();

            services.AddDbContext<CocktailMagicianContext>(options =>
           options
           .UseSqlServer(
               Configuration.GetConnectionString("DefaultConnection")));

            //bars services
            //bars services

            services.AddScoped<IBarService, BarService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IDtoMapper<Bar, BarDTO>, BarDTOMapper>();
            services.AddScoped<IViewModelMapper<BarDTO, BarViewModel>,BarViewModelMapper>();

            //cocktails services
            //cocktails services


            services.AddScoped<ICocktailService, CocktailService>();
            services.AddScoped<IDtoMapper<Cocktail, CocktailDto>, CocktailDtoMapper>();
            services.AddScoped<IViewModelMapper<CocktailDto, CocktailViewModel>, CocktailViewModelMapper>();

            services.AddScoped<IDtoMapper<CocktailComment, CocktailCommentDto>, CocktailCommentDtoMapper>();
            services.AddScoped<ICocktailCommentService, CocktailCommentService>();
            
            services.AddScoped<IDtoMapper<CocktailRating, CocktailRatingDto>, CocktailRatingDtoMapper>();
            services.AddScoped<ICocktailRatingService, CocktailRatingService>();

            //ingredients services
            //ingredients services

            services.AddScoped<IDtoMapper<Ingredient, IngredientDto>, IngredientDtoMapper>();
            services.AddScoped<IIngredientService, IngredientService>();
            services.AddScoped<ICocktailIngredientService, CocktailIngredientService>();
            
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
            services.AddControllersWithViews();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
