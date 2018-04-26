using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using WebKantora.Data.Models.Contracts;

namespace WebKantora.Data.Models
{
    public class User: IdentityUser<Guid>, IEntity, IDeletable
    {
        public User()
        {
            this.Id = Guid.NewGuid();
            this.Messages = new HashSet<Message>();
        }

        [Key]
        public override Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
        public virtual ICollection<Message> Messages { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
