using Core.Constants;

namespace Core.Requests;

public class CreateSavingAccount
{
    public SavingType SavingType { get; set; }

    public string HolderName { get; set; } = string.Empty;
}