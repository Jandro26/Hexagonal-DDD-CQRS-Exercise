using Hexagonal_Exercise.catalog.product.domain;
using Hexagonal_Exercise.core.application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.catalog.product.application.find
{
    public class FindProductQueryHandler: QueryHandler<FindProductQuery, FindProductQueryResult>
    {
        private readonly ProductFinder productFinder;
        private readonly FindProductResultConverter productResponseConverter;
        public FindProductQueryHandler(ProductRepository productRepository)
        {
            productFinder = new ProductFinder(productRepository);
            productResponseConverter = new FindProductResultConverter();
        }

        public async Task<FindProductQueryResult> Dispatch(FindProductQuery query, CancellationToken cancellationToken)
        {
            var id = new ProductId(query.Id);
            return await productResponseConverter.Map(productFinder.Execute(id).Result);
        }
    }
}
