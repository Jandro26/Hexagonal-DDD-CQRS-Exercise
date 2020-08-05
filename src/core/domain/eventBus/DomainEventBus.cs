
using System.Collections.Generic;

namespace Hexagonal_Exercise.core.domain.eventBus
{
    public interface DomainEventBus
    {
        public void Publish(IEnumerable<DomainEvent> events);
    }
}
