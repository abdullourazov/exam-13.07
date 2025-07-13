using Domain.ApiResponse;
using Domain.DTOs.Branch;

namespace Infrastructure.Interfaces;

public interface IBranchService
{
    Task<Response<string>> CreateBranchAsync(CreateBranchDto createBranchDto);
    Task<Response<GetBranchDto?>> GetBranchByIdAsync(int id);
    Task<Response<List<GetBranchDto>>> GetBranchesAsync();
    Task<Response<string>> UpdateBranchAsync(int id, UpdateBranchDto updateBranchDto);
    Task<Response<string>> DeleteBranchAsync(int id);
}
