using Domain.DTOs.Customer;
using Domain.Entities;

namespace Infrastructure.Mapper;

public static class CustomerMappers
{
    public static Customer ToEntity(CreateCustomerDto createCustomerDto)
    {
        return new Customer
        {
            FullName = createCustomerDto.FullName,
            Phone = createCustomerDto.Phone,
            Email = createCustomerDto.Email,
        };
    }

    public static void ToEntity(this Customer customer, UpdateCustomerDto updateCustomerDto)
    {
        customer.FullName = updateCustomerDto.FullName;
        customer.Phone = updateCustomerDto.Phone;
        customer.Email = updateCustomerDto.Email;
    }
}
