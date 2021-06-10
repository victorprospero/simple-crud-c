using Inventory.Middleware.Exceptions;
using Inventory.Middleware.Services.Interface;
using Inventory.Middleware.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Inventory.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        protected readonly IService<Product> _service;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger, IService<Product> service)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("Create")]
        public IActionResult Create(Product newProduct)
        {
            _logger.LogInformation("Creating product");
            try
            {
                _service.Create(newProduct);
                return Ok();
            }
            catch (ProductNotValidException notValidError)
            {
                _logger.LogInformation("Error on creating product: {0}", notValidError.Message);
                return BadRequest(notValidError.Message);
            }
            catch (ProductAlreadyExistsExeption alreadyExistsException)
            {
                _logger.LogInformation("Error on creating product: {0}", alreadyExistsException.Message);
                return BadRequest(alreadyExistsException.Message);
            }
        }

        [HttpGet("Retrieve")]
        public IActionResult Retrieve(uint sku)
        {
            _logger.LogInformation("Retrieving product");
            try
            {
                return Ok(_service.Retrieve(sku));
            }
            catch (ProductNotFoundExeption notFoundError)
            {
                _logger.LogInformation("Error on retrieving product: {0}", notFoundError.Message);
                return NotFound(notFoundError.Message);
            }
        }

        [HttpPut("Update")]
        public IActionResult Update(Product editingProduct)
        {
            _logger.LogInformation("Updating product");
            try
            {
                _service.Update(editingProduct);
                return Ok();
            }
            catch (ProductNotValidException notValidError)
            {
                _logger.LogInformation("Error on updating product: {0}", notValidError.Message);
                return BadRequest(notValidError.Message);
            }
            catch (ProductNotFoundExeption notFoundError)
            {
                _logger.LogInformation("Error on updating product: {0}", notFoundError.Message);
                return NotFound(notFoundError.Message);
            }
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(uint sku)
        {
            _logger.LogInformation("Deleting product");
            try
            {
                _service.Delete(sku);
                return Ok();
            }
            catch (ProductNotFoundExeption notFoundError)
            {
                _logger.LogInformation("Error on deleting product: {0}", notFoundError.Message);
                return NotFound(notFoundError.Message);
            }
        }
    }
}