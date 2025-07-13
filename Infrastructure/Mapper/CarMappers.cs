using Domain.DTOs.Car;
using Domain.Entities;

namespace Infrastructure.Mapper;

public static class CarMappers
{
    public static Car ToEntity(CreateCarDto createCarDto)
    {
        return new Car
        {
            Model = createCarDto.Model,
            Manufacturer = createCarDto.Manufacturer,
            Year = createCarDto.Year,
            PricePerDay = createCarDto.PricePerDay
        };
    }


    public static void ToEntity(this Car car, UpdateCarDto updateCarDto)
    {
        car.Model = updateCarDto.Model;
        car.Manufacturer = updateCarDto.Manufacturer;
        car.Year = updateCarDto.Year;
        car.PricePerDay = updateCarDto.PricePerDay;
    }
}
