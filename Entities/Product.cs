using System.ComponentModel.DataAnnotations;

namespace service.Entities
{
    public class Product
    {
        public string Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public int Stock { get; set; }

        public Category category { set; get; }
    }
}