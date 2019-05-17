using System;

namespace EventSourcing
{
    public interface IEvent
    {
        Guid EventId { get; }
    }
}
