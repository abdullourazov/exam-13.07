using Domain.DTOs.Car;
using Domain.DTOs.Customer;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RentalRepository(DataContext context) : IRentalRepository
{
    public async Task<int> CreateAsync(Rental rental)
    {
        await context.Rentals.AddAsync(rental);
        return await context.SaveChangesAsync();
    }


    public async Task<List<Rental>> GetAllAsync()
    {
        return await context.Rentals.ToListAsync();
    }

    public async Task<List<Rental>> GetByCarIdAsync(int carId)
    {
        return await context.Rentals
            .Where(r => r.CarId == carId)
            .Include(r => r.Car)
            .Include(r => r.Customer)
            .Include(r => r.Branch)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Rental>> GetByCustomerIdAsync(int customerId)
    {
        return await context.Rentals
            .Where(r => r.CustomerId == customerId)
            .Include(r => r.Car)
            .Include(r => r.Customer)
            .Include(r => r.Branch)
            .AsNoTracking()
            .ToListAsync();
    }


    public async Task<Rental?> GetByIdAsync(int id)
    {
        var rental = await context.Rentals.FindAsync(id);
        return rental;
    }
    public async Task<bool> IsCarAvailableAsync(int carId, DateTime startDate, DateTime endDate)
    {
        var isBusy = await context.Rentals.AnyAsync(r =>
            r.CarId == carId &&
            r.StartDate < endDate &&
            r.EndDate > startDate
        );
        return !isBusy;
    }

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

    public async Task<List<TopModelDto>> GetTop5PopularModelAsync(int year, int month)
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

}
