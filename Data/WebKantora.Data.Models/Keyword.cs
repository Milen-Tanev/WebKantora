using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebKantora.Data.Models
{
    public class Keyword
    {
        public Keyword()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string Content { get; set; }

        public ICollection<KeywordArticle> KeywordArticles { get; set; }
    }
}