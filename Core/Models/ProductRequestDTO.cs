using Core.Constants;
using Core.Entities;
using Core.Request;

namespace Core.Models;

public class ProductRequestDTO
{
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? Description { get; set; }

    public string Currency { get; set; } = null!;
    public string Customer { get; set; } = null!;

    public DateTime ApplicationDate { get; set; }
    public DateTime? ApprovalDate { get; set; }

}
