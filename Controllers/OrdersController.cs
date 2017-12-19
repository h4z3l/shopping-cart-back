using Microsoft.AspNetCore.Mvc;
using service.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System;
using System.Threading.Tasks;

namespace service.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        public readonly ApplicationDbContext _context;
        public readonly UserManager<ApplicationUser> _userManager;
        
        public OrdersController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager
        )
        {
            _context = context;
            _userManager = userManager;
        }
        
        [HttpPost]
        [Authorize]
        public async Task<object> Create([FromBody] OrderObject orderObject)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user == null)
            {
                return NotFound("User not found");
            }

            List<OrderItem> orderItems = new List<OrderItem>();

            for (var i=0; i<orderObject.items.Length; i++)
            {
                orderItems.Add(new OrderItem {
                    Product = _context.Products.FirstOrDefault(t => t.Id == orderObject.items[i].ProductId),
                    Quantity = orderObject.items[i].Quantity
                });
            }

            var order = new Order {
                User = user,
                CreditCardNumber = orderObject.order.CreditCardNumber,
                SecurityCode = orderObject.order.SecurityCode,
                ExpirationDate = orderObject.order.ExpirationDate,
                Items = orderItems
            };
            
            _context.Orders.Add(order);
            _context.SaveChanges();

            return new ObjectResult(order);
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.ToList();
        }
    }

    public class OrderObject
    {
        [Required]
        public Order order;

        [Required]
        public OrderItemDto[] items;
    }

    public class OrderItemDto
    {
        [Required]
        public string ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}