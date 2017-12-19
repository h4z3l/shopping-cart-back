using System.ComponentModel.DataAnnotations;

namespace service.Entities
{
    public class OrderItem
    {
        public string Id { get; set; }

        [Required]
        public Product Product { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}