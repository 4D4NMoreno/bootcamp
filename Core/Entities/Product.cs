namespace Core.Entities;

public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public ICollection<ProductRequest> ProductRequests { get; set; } = new List<ProductRequest>();
}
