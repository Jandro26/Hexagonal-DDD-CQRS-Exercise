using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Hexagonal_Exercise.catalog.core.domain.commandBus;
using Hexagonal_Exercise.catalog.product.application.create;
using Hexagonal_Exercise.catalog.product.application.delete;
using Hexagonal_Exercise.catalog.product.application.find;
using Hexagonal_Exercise.catalog.product.application.update;
using Hexagonal_Exercise.core.domain.queryBus;
using Hexagonal_Exercise.entry_point.catalog.v1.model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hexagonal_Exercise.entry_point.catalog.v1.controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ICommandDispacher _commandBus;
        private readonly IQueryDispacher _queryBus;
        private readonly IMapper _mapper;

        public ProductsController(ICommandDispacher commandBus, IQueryDispacher queryBus, IMapper mapper)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
            _mapper = mapper;
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
                await _commandBus.Dispatch(productCommand, cancellationToken).ConfigureAwait(false);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to create a new product in the database.
        /// </summary>
        /// <param name="renameProductModel">Model to rename a product</param>
        /// <returns>Nothing</returns>
        /// /// <response code="200">Returned if the product was created</response>
        /// /// <response code="400">Returned if the model couldn't be parsed or the product couldn't be saved</response>
        /// /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPut]
        public async Task<IActionResult> Put(int productId, [FromBody] RenameProductModel renameProductModel, CancellationToken cancellationToken = default)
        {
            try
            {
                var productCommand = new RenameProductCommand(productId, renameProductModel.Name);
                await _commandBus.Dispatch(productCommand, cancellationToken).ConfigureAwait(false);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to delete a product in the database.
        /// </summary>
        /// <param name="deleteProductModel">Model to delete a product</param>
        /// <returns>Nothing</returns>
        /// /// <response code="200">Returned if the product was created</response>
        /// /// <response code="400">Returned if the model couldn't be parsed or the product couldn't be saved</response>
        /// /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int productId, CancellationToken cancellationToken = default)
        {
            try
            {
                var productCommand = new DeleteProductCommand(productId);
                await _commandBus.Dispatch(productCommand, cancellationToken).ConfigureAwait(false);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to get a product by one id from the database.
        /// </summary>
        /// <param name="productId">Model to get a product by product id</param>
        /// <returns>Returns one product "GetProductByIdResultModel"</returns>
        /// /// <response code="200">Returned if the product is returned</response>
        /// /// <response code="400">Returned if the model couldn't be parsed or the product couldn't be find</response>
        /// /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpGet]
        public async Task<ActionResult<GetProductByIdResultModel>> Get(int productId, CancellationToken cancellationToken = default)
        {
            try
            {
                var query = new FindProductQuery(productId);
                var response = await _queryBus.Dispatch<FindProductQuery, FindProductQueryResult>(query, cancellationToken).ConfigureAwait(false);
                return Ok(_mapper.Map< GetProductByIdResultModel>(response));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
