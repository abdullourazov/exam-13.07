using Domain.ApiResponse;
using Domain.DTOs.Car;
using Domain.DTOs.Rental;

namespace Infrastructure.Interfaces;

public interface IRentalService
{
    Task<Response<string>> CreateRentalAsync(CreateRentalDto createRentalDto);
    Task<Response<GetRentalDto?>> GetRentalByIdAsync(int id);
    Task<Response<List<GetRentalDto>>> GetRentalsAsync();
    Task<Response<List<GetRentalDto>>> GetRentalsByCustomerIdAsync(int customerId);
    Task<Response<List<GetRentalDto>>> GetRentalsByCarIdAsync(int carId);
    Task<Response<decimal>> GetTotalRevenueAsync(DateTime startDate, DateTime endDate);
    Task<Response<List<CarUsageDto>>> GetCarUsageAsync(DateTime startDate, DateTime endDate);

}
