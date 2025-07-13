using System.Net;
using AutoMapper;
using Domain.ApiResponse;
using Domain.DTOs.Customer;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CustomerService(DataContext context,
        ICustomerRepository customerRepository,
        IMapper mapper) : ICustomerService
{
    public async Task<Response<string>> CreateCustomerAsync(CreateCustomerDto createCustomerDto)
    {
        var customer = CustomerMappers.ToEntity(createCustomerDto);
        var result = await customerRepository.CreateAsync(customer);

        return result == 0
            ? Response<string>.Error(HttpStatusCode.InternalServerError, "Something went wrong")
            : Response<string>.Success(message: "Customer created successfully");
    }

    public async Task<Response<string>> DeleteCustomerAsync(int id)
    {
        var customer = await context.Customers.FindAsync(id);
        if (customer == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Customer not found");
        }

        context.Customers.Remove(customer);
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong")
            : new Response<string>(null, "Customer updated successfully");
    }

    public async Task<Response<GetCustomerDto?>> GetCustomerByIdAsync(int id)
    {
        var car = await context.Customers
          .AsNoTracking()
          .FirstOrDefaultAsync(c => c.Id == id);

        if (car == null)
        {
            return new Response<GetCustomerDto?>(HttpStatusCode.NotFound, "Customer not found");
        }

        var mapped = mapper.Map<GetCustomerDto>(car);
        return new Response<GetCustomerDto?>(mapped);
    }

    public async Task<Response<List<GetCustomerDto>>> GetCustomersAsync()
    {
        var customer = await context.Customers
          .AsNoTracking()
          .Select(b => new GetCustomerDto
          {
              Id = b.Id,
              FullName = b.FullName,
              Phone = b.Phone,
              Email = b.Email
          }).ToListAsync();

        return new Response<List<GetCustomerDto>>(customer);
    }

    public async Task<Response<string>> UpdateCustomerAsync(int id, UpdateCustomerDto updateCustomerDto)
    {
        var customer = await context.Customers.FindAsync(id);
        if (customer == null)
        {
            return Response<string>.Error(HttpStatusCode.NotFound, "Customer not found");
        }

        customer.ToEntity(updateCustomerDto);
        var result = await customerRepository.UpdateAsync(customer);

        return result == 0
            ? Response<string>.Error(HttpStatusCode.InternalServerError, "Something went wrong")
            : Response<string>.Success(null, "Customer updated successfully");
    }

}
