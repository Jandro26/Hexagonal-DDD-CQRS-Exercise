using Hexagonal_Exercise.catalog.product.application.find;
using Hexagonal_Exercise.catalog.product.domain;
using Hexagonal_Exercise.core.application;
using System.Threading;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.catalog.product.application.delete
{
    public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
    {
        private readonly ProductDeleter _productDeleter;
        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productDeleter = new ProductDeleter(productRepository);
        }

        public async Task Dispatch(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var id = new ProductId(command.Id);
            await _productDeleter.Execute(id).ConfigureAwait(false);
        }
    }
}
