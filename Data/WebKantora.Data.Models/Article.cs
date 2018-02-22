using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using WebKantora.Data.Models.Contracts;

namespace WebKantora.Data.Models
{
    public class Article: IEntity, IDeletable
    {
        public Article()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public virtual User Author { get; set; }
        
        // DateTime.UtcNow
        [Required]
        public DateTime Date { get; set; }
        
        //MinLength, MaxLength?
        [Required]
        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<KeywordArticle> KeywordArticles { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}