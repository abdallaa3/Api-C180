using Day4tackC180.Dtos;
using Day4tackC180.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day4tackC180.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            return await _context.Products
                                 .Select(p => new ProductDto
                                 {
                                     Id = p.Id,
                                     Name = p.Name,
                                     Price = p.Price,
                                     Description = p.Description,
                                     Amount = p.Amount,
                                     CatalogId = p.CatalogId
                                 }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Amount = product.Amount,
                CatalogId = product.CatalogId
            };
        }

        // Get Products by Price
        [HttpGet("price/{price}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByPrice(decimal price)
        {
            return await _context.Products
                                 .Where(p => p.Price == price)
                                 .Select(p => new ProductDto
                                 {
                                     Id = p.Id,
                                     Name = p.Name,
                                     Price = p.Price,
                                     Description = p.Description,
                                     Amount = p.Amount,
                                     CatalogId = p.CatalogId
                                 }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> AddProduct(ProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description,
                Amount = productDto.Amount,
                CatalogId = productDto.CatalogId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            productDto.Id = product.Id;
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, productDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditProduct(int id, ProductDto productDto)
        {
            if (id != productDto.Id) return BadRequest();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.Description = productDto.Description;
            product.Amount = productDto.Amount;
            product.CatalogId = productDto.CatalogId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
