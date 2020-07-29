using System;

namespace Hexagonal_Exercise.core.domain.eventBus
{
    public class DomainEvent
    {
        private Guid _id;

        public Guid Id { get { return _id; } }

        public DomainEvent()
        {
            _id = new Guid();
        }
    }
}
