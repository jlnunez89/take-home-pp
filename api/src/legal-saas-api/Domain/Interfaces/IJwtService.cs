namespace LegalSaaS.Api.Domain.Interfaces;

public interface IJwtService
{
    string GenerateToken(int userId, string email);
    int GetUserIdFromToken(string token);
}
