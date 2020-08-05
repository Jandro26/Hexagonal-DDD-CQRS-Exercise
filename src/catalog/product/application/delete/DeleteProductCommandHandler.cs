using Hexagonal_Exercise.catalog.product.application.find;
using Hexagonal_Exercise.catalog.product.domain;
using Hexagonal_Exercise.core.application;
using System.Threading;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.catalog.product.application.delete
{
    public class DeleteProductCommandHandler : CommandHandler<DeleteProductCommand>
    {
        private readonly ProductDeleter productDeleter;
        public DeleteProductCommandHandler(ProductRepository productRepository)
        {
            productDeleter = new ProductDeleter(productRepository);
        }

        public async Task Dispatch(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var id = new ProductId(command.Id);
            await productDeleter.Execute(id).ConfigureAwait(false);
        }
    }
}
