using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
namespace WebApplication1.Controllers;

public class OrderController : Controller
{
    private readonly ShopContext _context;
    public OrderController(ShopContext context)
    {
        _context = context;
    }
    
    public IActionResult Index()
    {
        List<Order> orders = _context.Orders.Include(o => o.Product).ToList();
        return View(orders);
    }
    
    public IActionResult Create(int id)
    {
        Product? product = _context.Products.FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        Order order = new Order
        {
            ProductId = product.Id,
            Product = product,
            Quantity = 1
        };
        return View(order);
    }

    [HttpPost]

    public IActionResult Create(Order order)
    {
        Product? product = _context.Products.FirstOrDefault(p => p.Id == order.ProductId);

        if (product == null)
        {
            return NotFound();
        }

        if (order.Quantity <= 0)
        {
            order.Quantity = 1;
        }
        
        order.Id = 0;

        order.Name = order.Name?.Trim() ?? "";
        order.Address = order.Address?.Trim() ?? "";
        order.ContactPhone = order.ContactPhone?.Trim() ?? "";
        order.ContactEmail = order.ContactEmail?.Trim() ?? "";
        order.Status = "Open";
        order.CreatedOn = DateTime.Now;
        order.TotalPrice = product.Price *  order.Quantity;
        
        _context.Orders.Add(order);
        _context.SaveChanges();
        
        
        return RedirectToAction("Index");
    }
}