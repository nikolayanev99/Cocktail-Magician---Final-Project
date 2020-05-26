using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Models;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Services.DtoMappers;
using CocktailMagician.Services.DtoMappers.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace CocktailMagician.Web.Utilities.ServiceRegistration
{
    public static class DtoMappersRegistration
    {
        public static IServiceCollection RegisterDtoMappers(this IServiceCollection services)
        {
            services.AddScoped<IDtoMapper<Bar, BarDTO>, BarDTOMapper>();
            services.AddScoped<IDtoMapper<BarComment, BarCommentDto>, BarCommentDtoMapper>();
            services.AddScoped<IDtoMapper<BarRating,BarRatingDto>,BarRatingDtoMapper>();

            services.AddScoped<IDtoMapper<Cocktail, CocktailDto>, CocktailDtoMapper>();
            services.AddScoped<IDtoMapper<CocktailComment, CocktailCommentDto>, CocktailCommentDtoMapper>();
            services.AddScoped<IDtoMapper<CocktailRating, CocktailRatingDto>, CocktailRatingDtoMapper>();

            services.AddScoped<IDtoMapper<Ingredient, IngredientDto>, IngredientDtoMapper>();

            return services;
        }
    }
}
