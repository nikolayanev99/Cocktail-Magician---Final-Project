﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Web.Models
{
    public class CheckBoxItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool isChecked { get; set; }
    }
}
