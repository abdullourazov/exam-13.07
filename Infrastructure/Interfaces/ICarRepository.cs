using Domain.ApiResponse;
using Domain.DTOs.Car;
using Domain.Entities;
using Domain.Filters;

namespace Infrastructure.Interfaces;

public interface ICarRepository
{
    Task<PagedResponse<List<Car>>> SearchAsync(CarFilter filter);
    Task<List<Car>> GetAllAsync();
    Task<Car?> GetByIdAsync(int id);
    Task<int> CreateAsync(Car car);
    Task<int> UpdateAsync(Car car);
    Task<int> DeleteAsync(Car car);

}
