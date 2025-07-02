using LegalSaaS.Api.Dal.Entities;

namespace LegalSaaS.Api.Domain.Interfaces;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(int id);
    Task<Customer> CreateAsync(Customer customer);
    Task<Customer> UpdateAsync(Customer customer);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
