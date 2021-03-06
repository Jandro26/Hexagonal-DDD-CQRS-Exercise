﻿
using Hexagonal_Exercise.catalog.product.domain;
using Hexagonal_Exercise.core.application;
using System.Threading;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.catalog.product.application.update
{
    public class RenameProductCommandHandler : CommandHandler<RenameProductCommand>
    {
        private readonly ProductRenamer productRenamer;
        public RenameProductCommandHandler(ProductRepository productRepository)
        {
            productRenamer = new ProductRenamer(productRepository);
        }

        public async Task Dispatch(RenameProductCommand command, CancellationToken cancellationToken)
        {
            var id = new ProductId(command.Id);
            var name = new ProductName(command.Name);
            await productRenamer.Execute(id, name).ConfigureAwait(false);
        }
    }
}
