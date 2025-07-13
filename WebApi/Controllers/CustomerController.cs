using Domain.ApiResponse;
using Domain.DTOs.Customer;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CustomerController(ICustomerService customerService) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto createCustomerDto)
    {
        var response = await customerService.CreateCustomerAsync(createCustomerDto);
        if (!response.IsSucces)
            return StatusCode(response.StatusCode, response.Message);

        return Ok(response.Message);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(int id)
    {
        var response = await customerService.GetCustomerByIdAsync(id);
        if (!response.IsSucces)
            return StatusCode(response.StatusCode, response.Message);

        return Ok(response.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomers()
    {
        var response = await customerService.GetCustomersAsync();
        if (!response.IsSucces)
            return StatusCode(response.StatusCode, response.Message);

        return Ok(response.Data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerDto updateCustomerDto)
    {
        var response = await customerService.UpdateCustomerAsync(id, updateCustomerDto);
        if (!response.IsSucces)
            return StatusCode(response.StatusCode, response.Message);

        return Ok(response.Message);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var response = await customerService.DeleteCustomerAsync(id);
        if (!response.IsSucces)
            return StatusCode(response.StatusCode, response.Message);

        return Ok(response.Message);
    }
}

