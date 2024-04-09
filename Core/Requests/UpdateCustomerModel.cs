namespace Core.Request;

public class UpdateCustomerModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Lastname { get; set; }
    public string? DocumentNumber { get; set; }
    public string? Address { get; set; }
    public string? Mail { get; set; }
    public string? Phone { get; set; }

    public string? CustomerStatus { get; set; }

    public DateTime Birth { get; set; }
    public int? BankId { get; set; }
}
