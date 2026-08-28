using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class BrandController : Controller
{
    private readonly ShopContext _context;
    
    public BrandController(ShopContext context)
    {
        _context = context;
    }
    
    public IActionResult Index()
    {
        List<Brand> brands = _context.Brands.Include(b => b.Products).ToList();
        return View(brands);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Brand brand)
    {
        brand.Name = brand.Name?.Trim() ?? "";
        brand.Country = brand.Country?.Trim() ?? "";

        bool exists = _context.Brands.Any(b => b.Name.ToLower() == brand.Name.ToLower());

        if (exists)
        {
            ModelState.AddModelError("Name", "Brand with that name already exists.");
        }

        if (!ModelState.IsValid)
        {
            return View(brand);
        }

        _context.Brands.Add(brand);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        Brand? brand = _context.Brands.FirstOrDefault(b => b.Id == id);

        if (brand == null)
        {
            return NotFound();
        }

        return View(brand);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Brand brand)
    {
        brand.Name = brand.Name?.Trim() ?? "";
        brand.Country = brand.Country?.Trim() ?? "";

        bool exists = _context.Brands.Any(b => b.Name.ToLower() == brand.Name.ToLower() && b.Id != brand.Id);

        if (exists)
        {
            ModelState.AddModelError("Name", "Brand with that name already exists.");
        }

        if (!ModelState.IsValid)
        {
            return View(brand);
        }

        _context.Brands.Update(brand);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        Brand? brand = _context.Brands.FirstOrDefault(b => b.Id == id);

        if (brand == null)
        {
            return NotFound();
        }
        
        bool hasProducts = _context.Products.Any(p => p.BrandId == id);

        if (hasProducts)
        {
            TempData["Message"] = "This brand has products and cannot be deleted";
            return RedirectToAction("Index");
        }

        _context.Brands.Remove(brand);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}