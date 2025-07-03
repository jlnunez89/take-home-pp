using LegalSaaS.Api.Domain.Interfaces;
using LegalSaaS.Api.Models.Requests;

namespace LegalSaaS.Api.Domain.Handlers;

public class MatterHandler : IMatterHandler
{
    private readonly IMatterRepository _matterRepository;
    private readonly ICustomerRepository _customerRepository;

    public MatterHandler(IMatterRepository matterRepository, ICustomerRepository customerRepository)
    {
        _matterRepository = matterRepository;
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<Models.Matter>> GetByCustomerIdAsync(int customerId)
    {
        // Verify customer exists
        var customerExists = await _customerRepository.ExistsAsync(customerId);
        if (!customerExists)
        {
            throw new KeyNotFoundException($"Customer with ID {customerId} not found");
        }

        var matters = await _matterRepository.GetByCustomerIdAsync(customerId);
        return matters.Select(MapToResponsePayload);
    }

    public async Task<Models.Matter> GetByIdAsync(int customerId, int matterId)
    {
        var matter = await _matterRepository.GetByIdAsync(customerId, matterId);
        if (matter == null)
        {
            throw new KeyNotFoundException($"Matter with ID {matterId} not found for customer {customerId}");
        }

        return MapToResponsePayload(matter);
    }

    public async Task<Models.Matter> CreateAsync(int customerId, CreateMatterRequestPayload request)
    {
        // Verify customer exists
        var customerExists = await _customerRepository.ExistsAsync(customerId);
        if (!customerExists)
        {
            throw new KeyNotFoundException($"Customer with ID {customerId} not found");
        }

        var matter = new Dal.Entities.Matter
        {
            Title = request.Title,
            Description = request.Description,
            Status = request.Status,
            CustomerId = customerId
        };

        var createdMatter = await _matterRepository.CreateAsync(matter);
        return MapToResponsePayload(createdMatter);
    }

    private static Models.Matter MapToResponsePayload(Dal.Entities.Matter matter)
    {
        return new Models.Matter
        {
            Id = matter.Id,
            Title = matter.Title,
            Description = matter.Description,
            Status = matter.Status,
            CustomerId = matter.CustomerId,
            CreatedAt = matter.CreatedAt,
            UpdatedAt = matter.UpdatedAt
        };
    }
}
