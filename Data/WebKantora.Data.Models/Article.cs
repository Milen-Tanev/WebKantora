using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebKantora.Data.Models
{
    public class Article
    {
        public Article()
        {
            this.Id = Guid.NewGuid();
            this.Keywords = new HashSet<Keyword>();
        }
        [Key]
        public Guid Id { get; set; }

        [Required]
        public virtual User Author { get; set; }
        
        // DateTime.UtcNow
        [Required]
        public DateTime Date { get; set; }

        public virtual ICollection<Keyword> Keywords { get; set; }

        //MinLength, MaxLength?
        [Required]
        public string Content { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}