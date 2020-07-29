
using System.Collections.Generic;

namespace Hexagonal_Exercise.core.domain.eventBus
{
    public interface IDomainEventBus
    {
        public void Publish(IEnumerable<DomainEvent> events);
    }
}
