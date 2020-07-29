using Hexagonal_Exercise.catalog.product.domain;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.catalog.product.application.find
{
    public class FindProductResultConverter
    {
        public async Task<FindProductQueryResult> Map(Product product) 
            => await Task.FromResult(new FindProductQueryResult(product));
    }
}
