using Domain.ApiResponse;
using Domain.DTOs.Car;
using Domain.Entities;
using Domain.Filters;
using Domain.Paginations;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CarRepository(DataContext context) : ICarRepository
{
    public async Task<int> CreateAsync(Car car)
    {
        await context.Cars.AddAsync(car);
        return await context.SaveChangesAsync();
    }

    public async Task<List<Car>> GetAllAsync()
    {
        return await context.Cars.ToListAsync();
    }

    public async Task<Car?> GetByIdAsync(int id)
    {
        var car = await context.Cars.FindAsync(id);
        return car;
    }

    public async Task<int> UpdateAsync(Car car)
    {
        context.Cars.Update(car);
        return await context.SaveChangesAsync();
    }
    public async Task<int> DeleteAsync(Car car)
    {
        context.Cars.Remove(car);
        return await context.SaveChangesAsync();
    }

    public async Task<PagedResponse<List<Car>>> SearchAsync(CarFilter filter)
    {
        var query = context.Cars.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Model))
        {
            query = query.Where(c => c.Model.ToLower().Trim().Contains(filter.Model.ToLower().Trim()));
        }

        if (!string.IsNullOrEmpty(filter.Manufacturer))
        {
            query = query.Where(c => c.Manufacturer.ToLower().Trim().Contains(filter.Manufacturer.ToLower().Trim()));
        }

        if (filter.Year.HasValue)
        {
            query = query.Where(c => c.Year == filter.Year.Value);
        }

        var pagination = new Pagination<Car>(query);
        return await pagination.GetPagedResponseAsync(filter.PageNumber, filter.PageSize);
    }



}
