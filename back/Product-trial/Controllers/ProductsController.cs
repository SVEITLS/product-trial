using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product_trial.BLL.Exceptions;
using Product_trial.BLL.Services;
using Product_trial.DAL.Models;

namespace Product_trial.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _productService;

        public ProductsController(ILogger<ProductsController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="product">The product to create.</param>
        /// <returns>The created product with location header.</returns>
        /// <response code="201">Returns the newly created product.</response>
        /// <response code="500">If an error occurred while creating the product.</response>
        [Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            try
            {
                Product createdProduct = await _productService.CreateAsync(product);
                return CreatedAtAction("Get", new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failure to create a new product");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A list of all products.</returns>
        /// <response code="200">Returns the list of products.</response>
        /// <response code="500">If an error occurred while retrieving the products.</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _productService.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failure to get all products");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Retrieves a specific product by ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The requested product.</returns>
        /// <response code="200">Returns the requested product.</response>
        /// <response code="404">If the product is not found.</response>
        /// <response code="500">If an error occurred while retrieving the product.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Product? product = await _productService.GetByIdAsync(id);
                if (product is null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failure to get the desired product with id : {Id}", id);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="patchedProduct">The updated product data.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="200">If the product was successfully updated.</response>
        /// <response code="404">If the product was not found.</response>
        /// <response code="500">If an error occurred while updating the product.</response>
        [Authorize(Policy = "Admin")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product patchedProduct)
        {
            try
            {
                await _productService.UpdateAsync(id, patchedProduct);
                return Ok();
            }
            catch (ProductMissingException ex)
            {
                _logger.LogError(ex, "Failure to update the desired product with id : {Id}, it does not exist", id);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failure to update the desired product with id : {Id}", id);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Deletes a specific product by ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="200">If the product was successfully deleted.</response>
        /// <response code="404">If the product was not found.</response>
        /// <response code="500">If an error occurred while deleting the product.</response>
        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _productService.DeleteAsync(id);
                return Ok();
            }
            catch (ProductMissingException ex)
            {
                _logger.LogError(ex, "Failure to delete the desired product with id : {Id}, it does not exist", id);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failure to delete the desired product with id : {Id}", id);
                return StatusCode(500);
            }
        }
    }
}
