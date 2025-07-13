using Domain.DTOs.Account;
using Infrastructure.Account;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(IAccountCustomerService accountCustomerService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterCustomerDto registerCustomerDto)
    {
        var result = await accountCustomerService.RegisterAsync(registerCustomerDto);
        if (!result.Succeeded)
            return BadRequest(result.Errors);
        return Ok("Customer registered successfully");
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginDto dto)
    {
        var token = await accountCustomerService.LoginAsync(dto);
        if (token == null)
            return Unauthorized();
        return Ok(new { token });
    }
}
