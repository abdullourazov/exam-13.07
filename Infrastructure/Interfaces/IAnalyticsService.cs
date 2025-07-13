using Domain.DTOs.Car;
using Domain.DTOs.Customer;

namespace Infrastructure.Interfaces;

public interface IAnalyticsService
{
    Task<decimal> GetTotalRevenueAsync(DateTime startDate, DateTime endDate);
    Task<List<CarUsageDto>> GetCarUsageAsync(DateTime startDate, DateTime endDate);
    Task<List<TopModelDto>> GetTop5PopularModelsAsync(int year, int month);
    Task<List<TopCustomerDto>> GetTopCustomersAsync(int count = 5);
}
