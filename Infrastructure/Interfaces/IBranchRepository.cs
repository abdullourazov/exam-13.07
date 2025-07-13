using Domain.ApiResponse;
using Domain.DTOs.Car;
using Domain.Entities;
using Domain.Filters;

namespace Infrastructure.Interfaces;

public interface IBranchRepository
{
    Task<List<Branch>> GetAllAsync();
    Task<Branch?> GetByIdAsync(int id);
    Task<int> CreateAsync(Branch branch);
    Task<int> UpdateAsync(Branch branch);
    Task<int> DeleteAsync(Branch branch);
}
