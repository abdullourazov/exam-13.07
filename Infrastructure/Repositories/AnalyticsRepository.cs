using Domain.DTOs.Car;
using Domain.DTOs.Customer;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;


public class AnalyticsService(DataContext context) : IAnalyticsService
{
    public async Task<decimal> GetTotalRevenueAsync(DateTime startDate, DateTime endDate)
    {
        return new decimal(null);
    }

    public async Task<List<CarUsageDto>> GetCarUsageAsync(DateTime startDate, DateTime endDate)
    {
        return new List<CarUsageDto>(null);
    }

    public async Task<List<TopCustomerDto>> GetTopCustomersAsync(int count = 5)
    {
        return new List<TopCustomerDto>(null);
    }

    public async Task<List<TopModelDto>> GetTop5PopularModelsAsync(int year, int month)
    {
        return new List<TopModelDto>(null);
    }
}