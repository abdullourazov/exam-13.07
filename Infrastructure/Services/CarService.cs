using System.Net;
using AutoMapper;
using Domain.ApiResponse;
using Domain.DTOs.Car;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CarService(DataContext context,
        ICarRepository carRepository,
        IMapper mapper) : ICarService
{
    public async Task<Response<string>> CreateCarAsync(CreateCarDto createCarDto)
    {
        var car = CarMappers.ToEntity(createCarDto);
        var result = await carRepository.CreateAsync(car);

        return result == 0
            ? Response<string>.Error(HttpStatusCode.InternalServerError, "Something went wrong")
            : Response<string>.Success(message: "Car created successfully");
    }

    public async Task<Response<string>> DeleteCarAsync(int id)
    {
        var car = await context.Cars.FindAsync(id);
        if (car == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Car not found");
        }

        context.Cars.Remove(car);
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong")
            : new Response<string>(null, "Car updated successfully");
    }

    public async Task<Response<GetCarDto?>> GetCarByIdAsync(int id)
    {
        var car = await context.Cars
          .AsNoTracking()
          .FirstOrDefaultAsync(c => c.Id == id);

        if (car == null)
        {
            return new Response<GetCarDto?>(HttpStatusCode.NotFound, "Car not found");
        }

        var mapped = mapper.Map<GetCarDto>(car);
        return new Response<GetCarDto?>(mapped);
    }

    public async Task<Response<List<GetCarDto>>> GetCarsAsync()
    {
        var car = await context.Cars
          .AsNoTracking()
          .Select(b => new GetCarDto
          {
              Id = b.Id,
              Model = b.Model,
              Manufacturer = b.Manufacturer,
              Year = b.Year,
              PricePerDay = b.PricePerDay
          }).ToListAsync();

        return new Response<List<GetCarDto>>(car);
    }

    public Task<Response<List<CarUsageDto>>> GetCarUsageAsync(DateTime startDate, DateTime endDate)
    {
        throw new NotImplementedException();
    }


    public async Task<Response<string>> UpdateCarAsync(int id, UpdateCarDto updateCarDto)
    {
        var car = await context.Cars.FindAsync(id);
        if (car == null)
        {
            return Response<string>.Error(HttpStatusCode.NotFound, "Car not found");
        }

        car.ToEntity(updateCarDto);
        var result = await carRepository.UpdateAsync(car);

        return result == 0
            ? Response<string>.Error(HttpStatusCode.InternalServerError, "Something went wrong")
            : Response<string>.Success(null, "Car updated successfully");
    }

}
