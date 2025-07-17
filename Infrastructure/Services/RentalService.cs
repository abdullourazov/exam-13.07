using System.Net;
using System.Net.Http.Headers;
using AutoMapper;
using Domain.ApiResponse;
using Domain.DTOs.Car;
using Domain.DTOs.Rental;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class RentalService(DataContext context,
        IRentalRepository rentalRepository,
        ICarRepository carRepository,
        IMapper mapper,
        IRedisCasheService redisCasheService) : IRentalService
{
    public async Task<Response<string>> CreateRentalAsync(CreateRentalDto createRentalDto)
    {
        var car = await carRepository.GetByIdAsync(createRentalDto.CarId);
        if (car == null)
            return Response<string>.Error(HttpStatusCode.NotFound, "Car not found");

        var days = (createRentalDto.EndDate - createRentalDto.StartDate).Days;
        if (days <= 0)
            return Response<string>.Error(HttpStatusCode.BadRequest, "Nevernyy srok arendy");

        var isAvailable = await rentalRepository.IsCarAvailableAsync(
            createRentalDto.CarId,
            createRentalDto.StartDate,
            createRentalDto.EndDate);

        if (!isAvailable)
            return Response<string>.Error(HttpStatusCode.Conflict, "Car is already rented in this period");

        var totalCost = car.PricePerDay * days;

        var rental = RentalMappers.ToEntity(createRentalDto);
        rental.TotalCost = totalCost;

        var result = await rentalRepository.CreateAsync(rental);

        await redisCasheService.DeleteData("rental");

        return result == 0
            ? Response<string>.Error(HttpStatusCode.InternalServerError, "Something went wrong")
            : Response<string>.Success(message: "Rental created successfully");
    }

    public Task<Response<List<CarUsageDto>>> GetCarUsageAsync(DateTime startDate, DateTime endDate)
    {
        throw new NotImplementedException();
    }


    public async Task<Response<GetRentalDto?>> GetRentalByIdAsync(int id)
    {
        var rental = await context.Rentals
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);

        if (rental == null)
            return new Response<GetRentalDto?>(HttpStatusCode.NotFound, "Rental not found");

        var mapped = mapper.Map<GetRentalDto>(rental);
        return new Response<GetRentalDto?>(mapped);
    }

    public async Task<Response<List<GetRentalDto>>> GetRentalsAsync()
    {
        const string cashKey = "rental";

        var rentalCashe = await redisCasheService.GetData<List<GetRentalDto>>(cashKey);

        if (rentalCashe == null)
        {
            var rental = await rentalRepository.GetAllAsync();
            rentalCashe = rental.Select(r => new GetRentalDto
            {
                Id = r.Id
            }).ToList();

            await redisCasheService.SetData(cashKey, rentalCashe, 1);
        }

        var rentals = await context.Rentals
            .AsNoTracking()
            .Include(r => r.Car)
            .Include(r => r.Customer)
            .Include(r => r.Branch)
            .ToListAsync();

        var mapped = mapper.Map<List<GetRentalDto>>(rentals);
        return new Response<List<GetRentalDto>>(mapped);
    }

    public async Task<Response<List<GetRentalDto>>> GetRentalsByCarIdAsync(int carId)
    {
        var rentals = await context.Rentals
        .Where(r => r.CarId == carId)
        .Include(r => r.Car)
        .Include(r => r.Customer)
        .Include(r => r.Branch)
        .AsNoTracking()
        .ToListAsync();

        var mapped = mapper.Map<List<GetRentalDto>>(rentals);
        return new Response<List<GetRentalDto>>(mapped);

    }

    public async Task<Response<List<GetRentalDto>>> GetRentalsByCustomerIdAsync(int customerId)
    {
        var rentals = await context.Rentals
        .Where(r => r.CustomerId == customerId)
        .Include(r => r.Car)
        .Include(r => r.Customer)
        .Include(r => r.Branch)
        .AsNoTracking()
        .ToListAsync();

        var mapped = mapper.Map<List<GetRentalDto>>(rentals);
        return new Response<List<GetRentalDto>>(mapped);
    }

    public async Task<Response<decimal>> GetTotalRevenueAsync(DateTime startDate, DateTime endDate)
    {
        var result = await context.Rentals
            .Where(r => r.StartDate >= startDate && r.EndDate <= endDate)
            .SumAsync(r => r.TotalCost);

        return new Response<decimal>(result);
    }

}
