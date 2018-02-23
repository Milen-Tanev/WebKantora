using System;
using System.ComponentModel.DataAnnotations;

using WebKantora.Data.Models.Contracts;

namespace WebKantora.Data.Models
{
    public class Message: IEntity, IDeletable
    {
        public Message()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        public User Author { get; set; }

        public string AuthorName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        
        [Required]
        public string Content { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
