using Microsoft.EntityFrameworkCore;
using LegalSaaS.Api.Dal.Database;
using LegalSaaS.Api.Dal.Entities;
using LegalSaaS.Api.Domain.Interfaces;

namespace LegalSaaS.Api.Dal.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _context.Customers
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await _context.Customers
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Customer> CreateAsync(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<Customer> UpdateAsync(Customer customer)
    {
        _context.Customers.Update(customer);

        await _context.SaveChangesAsync();

        return customer;
    }

    public async Task DeleteAsync(int id)
    {
        var customer = await GetByIdAsync(id);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Customers
            .AnyAsync(c => c.Id == id);
    }
}
