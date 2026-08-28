using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1.Controllers;

public class ProductController : Controller
{
    private readonly ShopContext _context;

    public ProductController(ShopContext context)
    {
        _context = context;
    }

    public IActionResult Index(int? categoryId, int? brandId)
    {
        var products = _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .AsQueryable();

        if (categoryId != null)
        {
            products = products.Where(p => p.CategoryId == categoryId);
        }

        if (brandId != null)
        {
            products = products.Where(p => p.BrandId == brandId);
        }

        ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name", categoryId);
        ViewBag.Brands = new SelectList(_context.Brands.ToList(), "Id", "Name", brandId);

        return View(products.ToList());
    }

    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
        ViewBag.Brands = new SelectList(_context.Brands.ToList(), "Id", "Name");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Product product)
    {
        product.Name = product.Name?.Trim() ?? "";
        product.Description = product.Description?.Trim() ?? "";
        product.ImageUrl = product.ImageUrl?.Trim() ?? "";

        bool categoryExists = _context.Categories.Any(c => c.Id == product.CategoryId);
        bool brandExists = _context.Brands.Any(b => b.Id == product.BrandId);

        if (!categoryExists)
        {
            ModelState.AddModelError("CategoryId", "Выберите существующую категорию");
        }

        if (!brandExists)
        {
            ModelState.AddModelError("BrandId", "Выберите существующий бренд");
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name", product.CategoryId);
            ViewBag.Brands = new SelectList(_context.Brands.ToList(), "Id", "Name", product.BrandId);

            return View(product);
        }

        product.CreatedOn = DateTime.UtcNow;
        product.ModifiedOn = DateTime.UtcNow;

        _context.Products.Add(product);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        Product? product = _context.Products.FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name", product.CategoryId);
        ViewBag.Brands = new SelectList(_context.Brands.ToList(), "Id", "Name", product.BrandId);

        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Product product)
    {
        product.Name = product.Name?.Trim() ?? "";
        product.Description = product.Description?.Trim() ?? "";
        product.ImageUrl = product.ImageUrl?.Trim() ?? "";

        bool categoryExists = _context.Categories.Any(c => c.Id == product.CategoryId);
        bool brandExists = _context.Brands.Any(b => b.Id == product.BrandId);

        if (!categoryExists)
        {
            ModelState.AddModelError("CategoryId", "Выберите существующую категорию");
        }

        if (!brandExists)
        {
            ModelState.AddModelError("BrandId", "Выберите существующий бренд");
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name", product.CategoryId);
            ViewBag.Brands = new SelectList(_context.Brands.ToList(), "Id", "Name", product.BrandId);

            return View(product);
        }

        Product? existingProduct = _context.Products.FirstOrDefault(p => p.Id == product.Id);

        if (existingProduct == null)
        {
            return NotFound();
        }

        existingProduct.Name = product.Name;
        existingProduct.Description = product.Description;
        existingProduct.Price = product.Price;
        existingProduct.Quantity = product.Quantity;
        existingProduct.ImageUrl = product.ImageUrl;
        existingProduct.CategoryId = product.CategoryId;
        existingProduct.BrandId = product.BrandId;
        existingProduct.ModifiedOn = DateTime.UtcNow;

        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult Details(int id)
    {
        Product? product = _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }
}