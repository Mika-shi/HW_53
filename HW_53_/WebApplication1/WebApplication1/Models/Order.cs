namespace WebApplication1.Models;

public class Order
{
    public int Id { get; set; }
    public string Name  { get; set; }  = "";
    public string Description  { get; set; } = "";
    public string Address { get; set; }  = "";
    public string ContactPhone { get; set; }  = "";
    public string ContactEmail { get; set; } = "";
    public DateTime CreatedOn { get; set; } =  DateTime.Now;
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; } = 1;
    public decimal TotalPrice { get; set; }
    public string Status { get; set; } = "Open";
}