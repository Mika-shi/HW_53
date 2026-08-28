using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Укажите название товара")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Название товара должно быть минимум 3 символа")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Укажите описание товара")]
    public string Description { get; set; } = "";

    [Required(ErrorMessage = "Укажите стоимость товара")]
    [Range(50, 1000000, ErrorMessage = "Стоимость не может быть меньше 50")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Укажите ссылку на изображение")]
    public string ImageUrl { get; set; } = "";

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "Выберите категорию")]
    public int CategoryId { get; set; }

    public Category? Category { get; set; } 

    [Required(ErrorMessage = "Выберите бренд")]
    public int BrandId { get; set; }

    public Brand? Brand { get; set; } 

    [Required(ErrorMessage = "Укажите количество")]
    [Range(1, 1000000, ErrorMessage = "Количество должно быть больше 0")]
    public int Quantity { get; set; }

    public List<Order> Orders { get; set; } = new List<Order>();
}