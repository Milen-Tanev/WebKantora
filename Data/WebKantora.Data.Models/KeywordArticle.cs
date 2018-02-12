using System;
using System.Collections.Generic;
using System.Text;

namespace WebKantora.Data.Models
{
    public class KeywordArticle
    {
        public Guid KeywordId { get; set; }
        public virtual Keyword Keyword { get; set; }
        public Guid ArticleId { get; set; }
        public virtual Article Article { get; set; }

    }
}
