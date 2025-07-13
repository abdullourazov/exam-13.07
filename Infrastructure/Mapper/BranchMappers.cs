using Domain.DTOs.Branch;
using Domain.Entities;

namespace Infrastructure.Mapper;

public static class BranchMappers
{
    public static Branch ToEntity(CreateBranchDto createBranchDto)
    {
        return new Branch
        {
            Name = createBranchDto.Name,
            Location = createBranchDto.Location
        };
    }


    public static void ToEntity(this Branch branch, UpdateBranchDto updateBranchDto)
    {
        branch.Name = updateBranchDto.Name;
        branch.Location = updateBranchDto.Location;
    }
}
