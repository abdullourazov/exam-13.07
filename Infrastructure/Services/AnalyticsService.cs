using Domain.DTOs.Car;
using Domain.DTOs.Customer;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class AnalyticsService(DataContext context) : IAnalyticsService
{
    public async Task<decimal> GetTotalRevenueAsync(DateTime startDate, DateTime endDate)
    {
        return await context.Rentals
            .Where(r => r.StartDate >= startDate && r.EndDate <= endDate)
            .SumAsync(r => r.TotalCost);
    }

    public async Task<List<CarUsageDto>> GetCarUsageAsync(DateTime startDate, DateTime endDate)
    {
        var totalDays = (endDate - startDate).TotalDays;

        var usage = await context.Rentals
            .Where(r => r.EndDate >= startDate && r.StartDate <= endDate)
            .GroupBy(r => r.CarId)
            .Select(g => new CarUsageDto
            {
                CarId = g.Key,
                UsagePercentage = g.Sum(r =>
                    (Math.Min(r.EndDate.Ticks, endDate.Ticks) - Math.Max(r.StartDate.Ticks, startDate.Ticks))
                    ) / TimeSpan.TicksPerDay / totalDays * 100
            }).ToListAsync();

        return usage;
    }

    public async Task<List<TopCustomerDto>> GetTopCustomersAsync(int count = 5)
    {
        return await context.Rentals
            .GroupBy(r => r.CustomerId)
            .OrderByDescending(g => g.Count())
            .Take(count)
            .Select(g => new TopCustomerDto
            {
                CustomerId = g.Key,
                RentalCount = g.Count()
            })
            .ToListAsync();
    }

    public async Task<List<TopModelDto>> GetTop5PopularModelsAsync(int year, int month)
    {
        var rentals = await context.Rentals
           .Where(r => r.StartDate.Year == year && r.StartDate.Month == month)
           .Include(r => r.Car)
           .GroupBy(r => r.Car.Model)
           .OrderByDescending(g => g.Count())
           .Take(5)
           .Select(g => new TopModelDto
           {
               Model = g.Key,
               RentalCount = g.Count()
           }).ToListAsync();

        return rentals;
    }
}


