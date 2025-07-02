using LegalSaaS.Api.Domain.Interfaces;
using LegalSaaS.Api.Models.Requests;

namespace LegalSaaS.Api.Domain.Handlers;

public class CustomerHandler : ICustomerHandler
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<Models.Customer>> GetAllAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        return customers.Select(MapToResponsePayload);
    }

    public async Task<Models.Customer> GetByIdAsync(int customerId)
    {
        var customer = await _customerRepository.GetByIdAsync(customerId);
        if (customer == null)
        {
            throw new KeyNotFoundException($"Customer with ID {customerId} not found");
        }

        return MapToResponsePayload(customer);
    }

    public async Task<Models.Customer> CreateAsync(CreateCustomerRequestPayload request)
    {
        var customer = new Dal.Entities.Customer
        {
            Name = request.Name,
            Phone = request.Phone
        };

        var createdCustomer = await _customerRepository.CreateAsync(customer);
        return MapToResponsePayload(createdCustomer);
    }

    public async Task<Models.Customer> UpdateAsync(int customerId, UpdateCustomerRequestPayload request)
    {
        var existingCustomer = await _customerRepository.GetByIdAsync(customerId);
        if (existingCustomer == null)
        {
            throw new KeyNotFoundException($"Customer with ID {customerId} not found");
        }

        existingCustomer.Name = request.Name;
        existingCustomer.Phone = request.Phone;
        existingCustomer.UpdatedAt = DateTimeOffset.UtcNow;

        var updatedCustomer = await _customerRepository.UpdateAsync(existingCustomer);
        return MapToResponsePayload(updatedCustomer);
    }

    public async Task DeleteAsync(int customerId)
    {
        var exists = await _customerRepository.ExistsAsync(customerId);
        if (!exists)
        {
            throw new KeyNotFoundException($"Customer with ID {customerId} not found");
        }

        await _customerRepository.DeleteAsync(customerId);
    }

    private static Models.Customer MapToResponsePayload(Dal.Entities.Customer customer)
    {
        return new Models.Customer
        {
            Id = customer.Id,
            Name = customer.Name,
            Phone = customer.Phone,
            CreatedAt = customer.CreatedAt,
            UpdatedAt = customer.UpdatedAt
        };
    }
}
