using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using service.Entities;


namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Product> GetAll([FromQuery] string name)
        {
            if (name != null)
            {
                var products = _context.Products.Where(t => t.Name.Contains(name));
                return products;
            }
            return _context.Products.ToList();
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetById(string id)
        {
            var item = _context.Products.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Product item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.Products.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetProduct", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Product item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var product = _context.Products.FirstOrDefault(t => t.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            product.Name = item.Name;
            product.Description = item.Description;
            product.Price = item.Price;
            product.ImageUrl = item.ImageUrl;
            product.Stock = item.Stock;

            _context.Products.Update(product);
            _context.SaveChanges();

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var item = _context.Products.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Products.Remove(item);
            _context.SaveChanges();

            return new NoContentResult();
        }
    }
}