using Hexagonal_Exercise.catalog.product.domain;
using Hexagonal_Exercise.core.application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.catalog.product.application.find
{
    public class FindProductQueryHandler: IQueryHandler<FindProductQuery, FindProductQueryResult>
    {
        private readonly ProductFinder _productFinder;
        private readonly FindProductResultConverter _productResponseConverter;
        public FindProductQueryHandler(IProductRepository productRepository)
        {
            _productFinder = new ProductFinder(productRepository);
            _productResponseConverter = new FindProductResultConverter();
        }

        public async Task<FindProductQueryResult> Dispatch(FindProductQuery query, CancellationToken cancellationToken)
        {
            var id = new ProductId(query.Id);
            return await _productResponseConverter.Map(_productFinder.Execute(id).Result);
        }
    }
}
