using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Category
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Укажите название категории")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Укажите описание категории")]
    public string Description { get; set; } = "";

    public List<Product> Products { get; set; } = new List<Product>();
}