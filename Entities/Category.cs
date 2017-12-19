using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace service.Entities
{
    public class Category
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string SubName { get; set; } 

        [Required]
        public string Description { get; set; }

        public List<Product> products { get; set; }
    }
}