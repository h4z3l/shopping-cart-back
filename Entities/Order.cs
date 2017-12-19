using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace service.Entities
{
    public class Order
    {
        public string Id { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public string CreditCardNumber { get; set; }

        [Required]
        public string SecurityCode { get; set; }

        [Required]
        public string ExpirationDate { get; set; }        

        [Required]
        public List<OrderItem> Items { get; set; }
    }
}