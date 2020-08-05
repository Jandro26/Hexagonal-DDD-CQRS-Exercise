using Hexagonal_Exercise.core.domain.eventBus;
using System.Collections.Generic;

namespace Hexagonal_Exercise.core.infrastructure
{
    public class DomainEventBusDefault : DomainEventBus
    {
        public void Publish(IEnumerable<DomainEvent> events)
        {
            //TODO
            //throw new NotImplementedException();
        }
    }
}
