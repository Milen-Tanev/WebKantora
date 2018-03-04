using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebKantora.Data.Models;

namespace WebKantora.Web.Areas.Administration.Models.BlogViewModels
{
    public class CreateArticleViewModel
    {
        public CreateArticleViewModel()
        {
            this.Keywords = new List<Keyword>();
        }

        [Required]
        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        //TODO: Min/Max Length
        [Required]
        [Display(Name = "Файл")]
        public IFormFile ArticleContent { get; set; }

        public ICollection<Keyword> AllKeywords { get; set; }

        [Display(Name = "Ключови думи")]
        public IList<Keyword> Keywords { get; set; }
    }
}
