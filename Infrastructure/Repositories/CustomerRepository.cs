using System.IO.Pipelines;
using System.Net.Security;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CustomerRepository(DataContext context) : ICustomerRepository
{
    public async Task<int> CreateAsync(Customer customer)
    {
        await context.Customers.AddAsync(customer);
        return await context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Customer customer)
    {
        context.Customers.Remove(customer);
        return await context.SaveChangesAsync();
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        return await context.Customers.ToListAsync();
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        var customer = await context.Customers.FindAsync(id);
        return customer;
    }

    public async Task<int> UpdateAsync(Customer customer)
    {
        context.Customers.Update(customer);
        return await context.SaveChangesAsync();
    }

}
