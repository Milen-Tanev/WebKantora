using System;
using System.Collections.Generic;

using WebKantora.Data.Models;

namespace WebKantora.Web.Models.ArticleViewModels
{
    public class ArticleViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public DateTime Date { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<KeywordArticle> KeywordArticles { get; set; }
    }
}
