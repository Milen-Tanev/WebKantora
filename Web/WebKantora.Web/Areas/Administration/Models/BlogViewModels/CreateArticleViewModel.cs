using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebKantora.Web.Areas.Administration.Models.BlogViewModels
{
    public class CreateArticleViewModel
    {
        public CreateArticleViewModel()
        {
            this.Keywords = new List<string>();
        }

        [Required]
        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        //TODO: Min/Max Length
        [Required]
        [Display(Name = "Файл")]
        public IFormFile ArticleContent { get; set; }

        public ICollection<string> AllKeywords { get; set; }

        [Display(Name = "Ключови думи")]
        public IList<string> Keywords { get; set; }

        //TODO: Min/Max Length
        [Required]
        [Display(Name = "Ключова дума")]
        public string Keyword { get; set; }
    }
}
