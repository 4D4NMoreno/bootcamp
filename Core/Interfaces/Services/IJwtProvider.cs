namespace Core.Interfaces.Services;

public interface IJwtProvider
{
    string Generate(IEnumerable<string> roles);
    //Dictionary<string, string> GenerateTokensForRoles(List<string> userRoles);
}
