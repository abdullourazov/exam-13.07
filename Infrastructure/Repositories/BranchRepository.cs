using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BranchRepository(DataContext context) : IBranchRepository
{
    public async Task<int> CreateAsync(Branch branch)
    {
        await context.Branches.AddAsync(branch);
        return await context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Branch branch)
    {
        context.Branches.Remove(branch);
        return await context.SaveChangesAsync();
    }

    public async Task<List<Branch>> GetAllAsync()
    {
        return await context.Branches.ToListAsync();
    }

    public async Task<Branch?> GetByIdAsync(int id)
    {
        var branch = await context.Branches.FindAsync(id);
        return branch;
    }

    public async Task<int> UpdateAsync(Branch branch)
    {
        context.Branches.Update(branch);
        return await context.SaveChangesAsync();
    }

}
