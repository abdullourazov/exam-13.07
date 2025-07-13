using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface ICustomerRepository
{
    Task<List<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(int id);
    Task<int> CreateAsync(Customer customer);
    Task<int> UpdateAsync(Customer customer);
    Task<int> DeleteAsync(Customer customer);
}
