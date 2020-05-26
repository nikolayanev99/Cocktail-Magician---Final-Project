using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Models;
using CocktailMagician.Services;
using CocktailMagician.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace CocktailMagician.Web.Utilities.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            
            services.AddScoped<IBarService, BarService>();
            services.AddScoped<IBarCommentsService, BarCommentsService>();
            services.AddScoped<IBarRatingService,BarRatingService>();
            
            services.AddScoped<ICocktailService, CocktailService>();
            services.AddScoped<ICocktailCommentService, CocktailCommentService>();
            services.AddScoped<ICocktailRatingService, CocktailRatingService>();

            services.AddScoped<IIngredientService, IngredientService>();
            services.AddScoped<ICocktailIngredientService, CocktailIngredientService>();

            return services;
        }
    }
}
