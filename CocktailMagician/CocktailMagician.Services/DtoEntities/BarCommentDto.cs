using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Services.DtoEntities
{
    public class BarCommentDto
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int BarId { get; set; }

        public int UserId { get; set; }

        public string Author { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }

    }
}
