using LegalSaaS.Api.Models;
using LegalSaaS.Api.Models.Requests;

namespace LegalSaaS.Api.Domain.Interfaces;

public interface ICustomerHandler
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer> GetByIdAsync(int customerId);
    Task<Customer> CreateAsync(CreateCustomerRequestPayload request);
    Task<Customer> UpdateAsync(int customerId, UpdateCustomerRequestPayload request);
    Task DeleteAsync(int customerId);
}
