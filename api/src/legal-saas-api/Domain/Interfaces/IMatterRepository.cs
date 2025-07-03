using LegalSaaS.Api.Dal.Entities;

namespace LegalSaaS.Api.Domain.Interfaces;

public interface IMatterRepository
{
    Task<IEnumerable<Matter>> GetByCustomerIdAsync(int customerId);
    Task<Matter?> GetByIdAsync(int customerId, int matterId);
    Task<Matter> CreateAsync(Matter matter);
    Task<bool> ExistsAsync(int customerId, int matterId);
}
