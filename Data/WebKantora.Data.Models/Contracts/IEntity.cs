using System;

namespace WebKantora.Data.Models.Contracts
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
