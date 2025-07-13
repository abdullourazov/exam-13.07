using Domain.DTOs.Car;
using Domain.DTOs.Customer;
using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface IRentalRepository
{
    Task<List<Rental>> GetAllAsync();
    Task<Rental?> GetByIdAsync(int id);
    Task<int> CreateAsync(Rental rental);
    Task<bool> IsCarAvailableAsync(int carId, DateTime startDate, DateTime endDate);
    Task<List<Rental>> GetByCarIdAsync(int carId);
    Task<List<Rental>> GetByCustomerIdAsync(int customerId);
    Task<decimal> GetTotalRevenueAsync(DateTime startDate, DateTime endDate);
    Task<List<CarUsageDto>> GetCarUsageAsync(DateTime startDate, DateTime endDate);
    Task<List<TopModelDto>> GetTop5PopularModelAsync(int year, int month);
    Task<List<TopCustomerDto>> GetTopCustomersAsync(int count = 5);



}
