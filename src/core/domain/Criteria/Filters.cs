using System.Collections.Generic;

namespace Hexagonal_Exercise.core.domain.Criteria
{
    public class Filters
    {
        private List<Filter> filters;
        public IEnumerable<Filter> GetFilters 
            => filters; 

        public void Add(Filter filter)
        {
            if (filters == null) filters = new List<Filter>();

            filters.Add(filter);
        }

        public void Remove(Filter filter)
        {
            if (filters == null || filters.Count == 0) return;

            filters.Remove(filter);
        }
    }
}
