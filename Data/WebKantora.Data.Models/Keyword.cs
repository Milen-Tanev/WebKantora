using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebKantora.Data.Models.Contracts;

namespace WebKantora.Data.Models
{
    public class Keyword: IEntity, IDeletable
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

        [Required]
        public bool IsDeleted { get; set; }
    }
}