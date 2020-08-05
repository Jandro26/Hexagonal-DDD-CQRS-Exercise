using System;

namespace Hexagonal_Exercise.core.domain.eventBus
{
    public class DomainEvent
    {

        public Guid Id { get ; }

        public DomainEvent()
        {
            Id = new Guid();
        }
    }
}
