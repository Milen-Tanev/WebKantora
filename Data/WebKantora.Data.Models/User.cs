using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using WebKantora.Data.Models.Contracts;
using System;

namespace WebKantora.Data.Models
{
    public class User: IdentityUser, IEntity, IDeletable
    {
        public User()
        {
            this.Messages = new HashSet<Message>();
        }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
        public virtual ICollection<Message> Messages { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        Guid IEntity.Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
