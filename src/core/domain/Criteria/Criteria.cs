using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.core.domain.Criteria
{
    public class Criteria
    {
        public Filters Filters { get; }
        public Order Order { get; }
        public int Offset { get;}
        public int Limit { get;}

        public Criteria(Filters filters, Order order, int offset, int limit)
        {
            Filters = filters;
            Order = order;
            Offset = offset;
            Limit = limit;

        }
    }
}
