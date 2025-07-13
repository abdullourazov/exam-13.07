using Domain.ApiResponse;
using Domain.DTOs.Customer;

namespace Infrastructure.Interfaces;

public interface ICustomerService
{
    Task<Response<string>> CreateCustomerAsync(CreateCustomerDto createCustomerDto);
    Task<Response<GetCustomerDto?>> GetCustomerByIdAsync(int id);
    Task<Response<List<GetCustomerDto>>> GetCustomersAsync();
    Task<Response<string>> UpdateCustomerAsync(int id, UpdateCustomerDto updateCustomerDto);
    Task<Response<string>> DeleteCustomerAsync(int id);
}
