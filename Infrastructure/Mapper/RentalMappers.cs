using Domain.DTOs.Rental;
using Domain.Entities;

namespace Infrastructure.Mapper;

public static class RentalMappers
{
    public static Rental ToEntity(CreateRentalDto createRentalDto)
    {
        return new Rental
        {
            CarId = createRentalDto.CarId,
            CustomerId = createRentalDto.CustomerId,
            BranchId = createRentalDto.BranchId,
            StartDate = createRentalDto.StartDate,
            EndDate = createRentalDto.EndDate,
            TotalCost = createRentalDto.TotalCost
        };
    }

}
