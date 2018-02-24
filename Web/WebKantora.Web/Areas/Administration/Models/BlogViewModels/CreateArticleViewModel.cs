using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebKantora.Web.Areas.Administration.Models.BlogViewModels
{
    public class CreateArticleViewModel
    {
        [Required]
        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        //TODO: Min/Max Length
        [Required]
        [Display(Name = "Текст")]
        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<string> Keywords { get; set; }
    }
}
