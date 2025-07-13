using System.Runtime.InteropServices;
using Domain.ApiResponse;
using Domain.DTOs.Account;
using Domain.DTOs.Customer;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Account;

public interface IAccountCustomerService
{

    Task<string?> LoginAsync(LoginDto loginDto);
    Task<IdentityResult> RegisterAsync(RegisterCustomerDto registerCustomerDto);
}
