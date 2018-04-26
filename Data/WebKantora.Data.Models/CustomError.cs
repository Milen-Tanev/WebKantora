using System;
using System.ComponentModel.DataAnnotations;
using WebKantora.Data.Models.Contracts;

namespace WebKantora.Data.Models
{
    public class CustomError : IEntity, IDeletable
    {
        public CustomError()
        {
            this.Id = Guid.NewGuid();
            this.ThrowTime = DateTime.UtcNow;
        }

        [Key]
        public Guid Id { get; set; }

        public string CustomMessage { get; set; }

        public string InnerException { get; set; }

        public string Source { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        [Required]
        public DateTime ThrowTime { get; private set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
