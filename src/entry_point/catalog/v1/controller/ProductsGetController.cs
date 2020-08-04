using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Hexagonal_Exercise.catalog.product.application.find;
using Hexagonal_Exercise.core.domain.queryBus;
using Hexagonal_Exercise.entry_point.catalog.v1.model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hexagonal_Exercise.entry_point.catalog.v1.controller
{
    [Route("api/Products")]
    [ApiController]
    public class ProductsGetController : ControllerBase
    {
        private readonly IQueryDispacher _queryBus;
        private readonly IMapper _mapper;

        public ProductsGetController(IQueryDispacher queryBus, IMapper mapper)
        {
            _queryBus = queryBus;
            _mapper = mapper;
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
