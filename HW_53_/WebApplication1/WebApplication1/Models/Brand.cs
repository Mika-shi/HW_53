using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Brand
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Укажите название бренда")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Укажите страну бренда")]
    public string Country { get; set; } = "";

    public List<Product> Products { get; set; } = new List<Product>();
}