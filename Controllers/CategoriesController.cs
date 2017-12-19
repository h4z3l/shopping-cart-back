using Microsoft.AspNetCore.Mvc;
using System.Linq;
using service.Entities;
using System.Collections.Generic;

namespace service.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public IEnumerable<Product> GetById(string id)
        {
            return _context.Products.Where(t => t.category.Id == id).ToList();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest();
            }

            _context.Categories.Add(category);
            _context.SaveChanges();

            return CreatedAtRoute("GetCategory", new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Category category)
        {
            if (category == null || category.Id != id)
            {
                return BadRequest();
            }

            var item = _context.Categories.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            item.Name = category.Name;
            item.SubName = category.SubName;
            item.Description = category.Description;

            _context.Categories.Update(item);
            _context.SaveChanges();

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var item = _context.Categories.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(item);
            _context.SaveChanges();

            return new NoContentResult();
        }
    }
}