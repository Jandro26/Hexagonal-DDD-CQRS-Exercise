using System;
using System.Threading;
using System.Threading.Tasks;
using Hexagonal_Exercise.catalog.core.domain.commandBus;
using Hexagonal_Exercise.catalog.product.application.create;
using Hexagonal_Exercise.entry_point.catalog.v1.model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hexagonal_Exercise.entry_point.catalog.v1.controller
{
    [Route("api/Products")]
    [ApiController]
    public class ProductsPostController : ControllerBase
    {
        private readonly CommandDispacher commandBus;

        public ProductsPostController(CommandDispacher commandBus)
        {
            this.commandBus = commandBus;
        }

        /// <summary>
        /// Action to create a new product in the database.
        /// </summary>
        /// <param name="createProductModel">Model to create a new product</param>
        /// <returns>Nothing</returns>
        /// /// <response code="200">Returned if the product was created</response>
        /// /// <response code="400">Returned if the model couldn't be parsed or the product couldn't be saved</response>
        /// /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductModel createProductModel, CancellationToken cancellationToken = default)
        {
            try
            {
                var productCommand = new CreateProductCommand(createProductModel.Id, createProductModel.Name);
                await commandBus.Dispatch(productCommand, cancellationToken).ConfigureAwait(false);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
