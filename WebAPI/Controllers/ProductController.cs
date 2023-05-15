using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository repository = new ProductRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return repository.GetProducts();
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var p = repository.GetProductById(id);
            if (p == null) return NotFound();
            return p;
        }

        [HttpPost]
        public IActionResult PostProduct(ProductRequestModel productRq)
        {
            var product = new Product
            {
                CategoryId = productRq.CategoryId,
                ProductId = productRq.ProductId,
                ProductName = productRq.ProductName,
                UnitPrice = productRq.UnitPrice,
                UnitsInStock = productRq.UnitsInStock
            };
            repository.SaveProduct(product);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, ProductRequestModel productRq)
        {
            var pTemp = repository.GetProductById(id);
            if (pTemp == null) return NotFound();

            var product = new Product
            {
                CategoryId = productRq.CategoryId,
                ProductId = productRq.ProductId,
                ProductName = productRq.ProductName,
                UnitPrice = productRq.UnitPrice,
                UnitsInStock = productRq.UnitsInStock
            };
            repository.UpdateProduct(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var p = repository.GetProductById(id);
            if (p == null) return NotFound();

            repository.DeleteProduct(id);
            return NoContent();
        }

        [HttpGet("category")]
        public ActionResult<IEnumerable<Category>> GetCategories()
        {
            return repository.GetCategories();
        }

        [HttpGet("category/{id}")]
        public ActionResult<Category> GetCategoryById(int id)
        {
            var c = repository.GetCategoryById(id);
            if (c == null) return NotFound();
            return c;
        }
    }
}
