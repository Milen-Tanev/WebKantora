using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebKantora.Data.Models
{
    public class Keyword
    {
        public Keyword()
        {
            this.Articles = new HashSet<Article>();
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
        
        [Required]
        public string Content { get; set; }
    }
}