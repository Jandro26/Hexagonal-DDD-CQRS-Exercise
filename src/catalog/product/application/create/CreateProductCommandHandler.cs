﻿using Hexagonal_Exercise.catalog.product.domain;
using Hexagonal_Exercise.core.application;
using Hexagonal_Exercise.core.domain.eventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.catalog.product.application.create
{
    public class CreateProductCommandHandler: ICommandHandler<CreateProductCommand>
    {
        private readonly ProductCreator _productCreator;

        public CreateProductCommandHandler(IProductRepository productRepository, IDomainEventBus eventBus) 
        {
            _productCreator = new ProductCreator(productRepository, eventBus);
        }

        public async Task Dispatch(CreateProductCommand productCommand, CancellationToken cancellationToken)
        {
            var id = new ProductId(productCommand.Id);
            var name = new ProductName(productCommand.Name);
            await _productCreator.Execute(id, name).ConfigureAwait(false);
        }
    }
}
