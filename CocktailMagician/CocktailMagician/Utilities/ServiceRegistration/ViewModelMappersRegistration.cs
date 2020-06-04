using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Models;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Web.Areas.Member.Models;
using CocktailMagician.Web.Mappers;
using CocktailMagician.Web.Mappers.Contracts;
using CocktailMagician.Web.Models;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.DependencyInjection;

namespace CocktailMagician.Web.Utilities.ServiceRegistration
{
    public static class ViewModelMappersRegistration
    {
        public static IServiceCollection RegisterViewModelMappers(this IServiceCollection services)
        {
            services.AddScoped<IViewModelMapper<BarDTO, BarViewModel>, BarViewModelMapper>();
            services.AddScoped<IViewModelMapper<BarCommentDto, BarCommentViewModel>, BarCommentViewModelMapper>();
            services.AddScoped<IViewModelMapper<BarRatingDto,BarRatingViewModel>,BarRatingViewModelMapper>();

            services.AddScoped<IViewModelMapper<CocktailDto, CocktailViewModel>, CocktailViewModelMapper>();
            services.AddScoped<IViewModelMapper<CocktailCommentDto, CocktailCommentViewModel>, CocktailCommentViewModelMapper>();
            services.AddScoped<IViewModelMapper<CocktailRatingDto, CocktailRatingViewModel>, CocktailRatingViewModelMapper>();

            services.AddScoped<IViewModelMapper<IngredientDto, IngredientViewModel>, IngredientViewModelMapper>();

            return services;
        }
    }
}
