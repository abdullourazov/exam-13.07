using System.Net;
using AutoMapper;
using Domain.ApiResponse;
using Domain.DTOs.Branch;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class BranchService(DataContext context,
        IBranchRepository branchRepository,
        IMapper mapper) : IBranchService
{
    public async Task<Response<string>> CreateBranchAsync(CreateBranchDto createBranchDto)
    {
        var branch = BranchMappers.ToEntity(createBranchDto);
        var result = await branchRepository.CreateAsync(branch);

        return result == 0
            ? Response<string>.Error(HttpStatusCode.InternalServerError, "Something went wrong")
            : Response<string>.Success(message: "Branch created successfully");
    }

    public async Task<Response<string>> DeleteBranchAsync(int id)
    {
        var branch = await context.Branches.FindAsync(id);
        if (branch == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Branch not found");
        }

        context.Branches.Remove(branch);
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong")
            : new Response<string>(null, "Branch updated successfully");
    }

    public async Task<Response<GetBranchDto?>> GetBranchByIdAsync(int id)
    {
        var branch = await context.Branches
           .AsNoTracking()
           .FirstOrDefaultAsync(c => c.Id == id);

        if (branch == null)
        {
            return new Response<GetBranchDto?>(HttpStatusCode.NotFound, "Branch not found");
        }

        var mapped = mapper.Map<GetBranchDto>(branch);
        return new Response<GetBranchDto?>(mapped);
    }

    public async Task<Response<List<GetBranchDto>>> GetBranchesAsync()
    {
        var branch = await context.Branches
           .AsNoTracking()
           .Select(b => new GetBranchDto
           {
               Id = b.Id,
               Name = b.Name,
               Location = b.Location,
           }).ToListAsync();

        return new Response<List<GetBranchDto>>(branch);
    }

    public async Task<Response<string>> UpdateBranchAsync(int id, UpdateBranchDto updateBranchDto)
    {
        var branch = await context.Branches.FindAsync(id);
        if (branch == null)
        {
            return Response<string>.Error(HttpStatusCode.NotFound, "Branch not found");
        }

        branch.ToEntity(updateBranchDto);
        var result = await branchRepository.UpdateAsync(branch);

        return result == 0
            ? Response<string>.Error(HttpStatusCode.InternalServerError, "Something went wrong")
            : Response<string>.Success(null, "Branch updated successfully");
    }

}
