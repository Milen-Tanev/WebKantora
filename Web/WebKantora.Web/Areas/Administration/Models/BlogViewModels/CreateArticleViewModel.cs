using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebKantora.Data.Models;

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
        public IFormFile ArticleContent { get; set; }

        public ICollection<Keyword> Keywords { get; set; }
    }
}
