using Core.Constants;
using Core.Entities;

namespace Core.Models;

public class SavingAccountDTO
{
    public int Id { get; set; }

    public string SavingType { get; set; }

    public string HolderName { get; set; } = string.Empty;
}
