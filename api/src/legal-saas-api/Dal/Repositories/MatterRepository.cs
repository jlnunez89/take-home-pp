using Microsoft.EntityFrameworkCore;
using LegalSaaS.Api.Dal.Database;
using LegalSaaS.Api.Dal.Entities;
using LegalSaaS.Api.Domain.Interfaces;

namespace LegalSaaS.Api.Dal.Repositories;

public class MatterRepository : IMatterRepository
{
    private readonly ApplicationDbContext _context;

    public MatterRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Matter>> GetByCustomerIdAsync(int customerId)
    {
        return await _context.Matters
            .Where(m => m.CustomerId == customerId)
            .OrderBy(m => m.Title)
            .ToListAsync();
    }

    public async Task<Matter?> GetByIdAsync(int customerId, int matterId)
    {
        return await _context.Matters
            .FirstOrDefaultAsync(m => m.Id == matterId && m.CustomerId == customerId);
    }

    public async Task<Matter> CreateAsync(Matter matter)
    {
        _context.Matters.Add(matter);
        await _context.SaveChangesAsync();
        return matter;
    }

    public async Task<bool> ExistsAsync(int customerId, int matterId)
    {
        return await _context.Matters
            .AnyAsync(m => m.Id == matterId && m.CustomerId == customerId);
    }
}
