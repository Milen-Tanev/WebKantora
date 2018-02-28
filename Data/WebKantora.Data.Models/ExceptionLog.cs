using System;
using System.ComponentModel.DataAnnotations;

namespace WebKantora.Data.Models
{
    public class CustomError: Exception
    {
        public CustomError()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        public string CustomMessage { get; set; }
    }
}
