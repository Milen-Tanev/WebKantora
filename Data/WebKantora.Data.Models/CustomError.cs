using System;
using System.ComponentModel.DataAnnotations;

namespace WebKantora.Data.Models
{
    public class CustomError
    {
        public CustomError()
        {
            this.Id = Guid.NewGuid();
            this.ThrowTIme = DateTime.UtcNow;
        }

        [Key]
        public Guid Id { get; set; }

        public string CustomMessage { get; set; }

        public string InnerException { get; set; }

        public string Source { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        [Required]
        public DateTime ThrowTIme { get; private set; }
    }
}
