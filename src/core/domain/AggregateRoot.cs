using Hexagonal_Exercise.core.domain.eventBus;
using System.Collections.Generic;

namespace Hexagonal_Exercise.catalog.core.domain
{
    public abstract class AggregateRoot
    {
        private IList<DomainEvent> _domainEvents = new List<DomainEvent>();

        public IEnumerable<DomainEvent> DomainEvents { get { return _domainEvents; } }

        protected void AddDomainEvent(DomainEvent domainEvent)
        {   
            _domainEvents.Add(domainEvent);
        }
    }
}
