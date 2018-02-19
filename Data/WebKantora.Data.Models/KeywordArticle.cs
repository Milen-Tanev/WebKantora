using System;
using System.ComponentModel.DataAnnotations;

using WebKantora.Data.Models.Contracts;

namespace WebKantora.Data.Models
{
    public class KeywordArticle: IDeletable
    {
        [Required]
        public Guid KeywordId { get; set; }

        [Required]
        public virtual Keyword Keyword { get; set; }

        [Required]
        public Guid ArticleId { get; set; }

        [Required]
        public virtual Article Article { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
