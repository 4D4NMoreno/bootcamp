namespace Core.Request;

public class FilterCustomersModel
{
    public int? BirthYearFrom { get; set; }
    public int? BirthYearTo { get; set; }
    public string? FullName { get; set; }
    public string DocumentNumber { get; set; } = string.Empty;
    public string? Mail { get; set; }
    public int? BankId { get; set; }
}