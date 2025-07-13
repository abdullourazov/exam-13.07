using Domain.ApiResponse;
using Domain.DTOs.Car;

namespace Infrastructure.Interfaces;

public interface ICarService
{
    Task<Response<string>> CreateCarAsync(CreateCarDto createCarDto);
    Task<Response<GetCarDto?>> GetCarByIdAsync(int id);
    Task<Response<List<GetCarDto>>> GetCarsAsync();
    Task<Response<string>> UpdateCarAsync(int id, UpdateCarDto updateCarDto);
    Task<Response<string>> DeleteCarAsync(int id);

}
