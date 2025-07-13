using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.ApiResponse;
using Domain.DTOs.Account;
using Infrastructure.Account;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace Infrastructure.Services;

public class AccountCustomerService(UserManager<IdentityUser> userManager,
        IConfiguration config,
        IHttpContextAccessor contextAccessor) : IAccountCustomerService
{
    public async Task<string?> LoginAsync(LoginDto loginDto)
    {
        var customer = await userManager.FindByNameAsync(loginDto.FullName);
        if (customer == null) return null;

        var result = await userManager.CheckPasswordAsync(customer, loginDto.Password);
        return !result
            ? null
            : GenerateJwtToken(customer);
    }

    private string GenerateJwtToken(IdentityUser user)
    {
        var claims = new List<Claim>()
            {
                new (ClaimTypes.NameIdentifier, user.Id),
                new (ClaimTypes.Name, user.UserName!)
            };

        var secretKey = config["Jwt:Key"]!;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<IdentityResult> RegisterAsync(RegisterCustomerDto registerCustomerDto)
    {
        var customer = new IdentityUser { UserName = registerCustomerDto.FullName };
        var result = await userManager.CreateAsync(customer, registerCustomerDto.Password);
        return result;
    }

}
