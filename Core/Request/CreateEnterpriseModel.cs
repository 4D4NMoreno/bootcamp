namespace Core.Request;

public class CreateEnterpriseModel
{

    public string Name { get; set; } = string.Empty;


    public string Address { get; set; } = string.Empty;

    public string? Phone { get; set; }

    public string? Email { get; set; }
}
