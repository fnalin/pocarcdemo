using Fansoft.PocArc.Api.Domain.Entities;

namespace Fansoft.PocArc.Api.Domain.Repository;

public interface ICustomerRepository
{
    Task<(IEnumerable<Customer> customers, int totalCount)> GetAllPagedAsync(int page, int pageSize, string search);
    Task<Customer> GetByIdAsync(Guid id);
    Task<Customer> AddAsync(Customer customer);
    Task<Customer> UpdateAsync(Customer customer);
    Task DeleteAsync(Guid id);
}