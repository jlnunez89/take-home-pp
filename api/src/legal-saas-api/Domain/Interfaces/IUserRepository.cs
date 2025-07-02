using LegalSaaS.Api.Dal.Entities;

namespace LegalSaaS.Api.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(int id);
    Task<User> CreateAsync(User user);
    Task<bool> EmailExistsAsync(string email);
}
