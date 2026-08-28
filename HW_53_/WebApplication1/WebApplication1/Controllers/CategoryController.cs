using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class CategoryController : Controller
{
    private readonly ShopContext _context;

    public CategoryController(ShopContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        List<Category> categories = _context.Categories
            .Include(c => c.Products)
            .ToList();

        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category)
    {
        category.Name = category.Name?.Trim() ?? "";
        category.Description = category.Description?.Trim() ?? "";

        bool exists = _context.Categories
            .Any(c => c.Name.ToLower() == category.Name.ToLower());

        if (exists)
        {
            ModelState.AddModelError("Name", "Category with that name already exists.");
        }

        if (!ModelState.IsValid)
        {
            return View(category);
        }

        _context.Categories.Add(category);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        Category? category = _context.Categories.FirstOrDefault(c => c.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category category)
    {
        category.Name = category.Name?.Trim() ?? "";
        category.Description = category.Description?.Trim() ?? "";

        bool exists = _context.Categories
            .Any(c => c.Name.ToLower() == category.Name.ToLower() && c.Id != category.Id);

        if (exists)
        {
            ModelState.AddModelError("Name", "Category with that name already exists.");
        }

        if (!ModelState.IsValid)
        {
            return View(category);
        }

        _context.Categories.Update(category);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        Category? category = _context.Categories.FirstOrDefault(c => c.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        bool hasProducts = _context.Products.Any(p => p.CategoryId == id);

        if (hasProducts)
        {
            TempData["Message"] = "This category has products and cannot be deleted";
            return RedirectToAction("Index");
        }

        _context.Categories.Remove(category);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}