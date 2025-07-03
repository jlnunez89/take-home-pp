using LegalSaaS.Api.Models;
using LegalSaaS.Api.Models.Requests;

namespace LegalSaaS.Api.Domain.Interfaces;

public interface IMatterHandler
{
    Task<IEnumerable<Matter>> GetByCustomerIdAsync(int customerId);
    Task<Matter> GetByIdAsync(int customerId, int matterId);
    Task<Matter> CreateAsync(int customerId, CreateMatterRequestPayload request);
}
