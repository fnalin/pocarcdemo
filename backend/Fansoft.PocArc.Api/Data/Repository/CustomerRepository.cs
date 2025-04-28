using Fansoft.PocArc.Api.Domain.Entities;
using Fansoft.PocArc.Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Fansoft.PocArc.Api.Data.Repository;

public class CustomerRepository (ApplicationDbContext context) : ICustomerRepository
{
    public async Task<(IEnumerable<Customer> customers, int totalCount)> GetAllPagedAsync(int page, int pageSize, string search)
    {
        var query = context.Customers.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var searchLower = search.ToLower();
            query = query.Where(c =>
                c.Name.ToLower().Contains(searchLower) ||
                c.Email.ToLower().Contains(searchLower));
        }

        var totalCount = await query.CountAsync();

        var customers = await query
            .OrderBy(c => c.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (customers, totalCount);
    }

    public async Task<Customer> GetByIdAsync(Guid id)
    {
        return await context.Customers.FindAsync(id);
    }

    public async Task<Customer> AddAsync(Customer customer)
    {
        context.Customers.Add(customer);
        await context.SaveChangesAsync();
        return customer;
    }

    public async Task<Customer> UpdateAsync(Customer customer)
    {
        var existingCustomer = await context.Customers.FindAsync(customer.Id);

        if (existingCustomer == null)
            throw new Exception("Customer not found.");

        existingCustomer.Name = customer.Name;
        existingCustomer.Email = customer.Email;
        existingCustomer.Phone = customer.Phone;

        await context.SaveChangesAsync();
        return customer;
    }

    public async Task DeleteAsync(Guid id)
    {
        var customer = await context.Customers.FindAsync(id);
        if (customer != null)
        {
            context.Customers.Remove(customer);
            await context.SaveChangesAsync();
        }
    }
}