using Hexagonal_Exercise.core.domain.eventBus;
using System.Collections.Generic;

namespace Hexagonal_Exercise.catalog.core.domain
{
    public abstract class AggregateRoot
    {
        private IList<DomainEvent> domainEvents = new List<DomainEvent>();

        public IEnumerable<DomainEvent> GetDomainEvents()
            => domainEvents;

        protected void AddDomainEvent(DomainEvent domainEvent)
        {   
            domainEvents.Add(domainEvent);
        }
    }
}
