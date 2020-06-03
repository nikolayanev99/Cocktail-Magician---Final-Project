using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Web.Models
{
    public class CreateCocktailViewModel
    {
        public CocktailViewModel CocktailViewModel { get; set; }
        public List<SelectListItem> AllIngredients { get; set; }
        public IFormFile Photo { get; set; }

    }
}
