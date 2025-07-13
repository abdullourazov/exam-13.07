using Domain.ApiResponse;
using Domain.DTOs.Rental;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RentalController(IRentalService rentalService) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateRental([FromBody] CreateRentalDto createRentalDto)
    {
        var response = await rentalService.CreateRentalAsync(createRentalDto);
        if (!response.IsSucces)
            return StatusCode(response.StatusCode, response.Message);

        return Ok(response.Message);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRentalById(int id)
    {
        var response = await rentalService.GetRentalByIdAsync(id);
        if (!response.IsSucces)
            return StatusCode(response.StatusCode, response.Message);

        return Ok(response.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetRentals()
    {
        var response = await rentalService.GetRentalsAsync();
        if (!response.IsSucces)
            return StatusCode(response.StatusCode, response.Message);

        return Ok(response.Data);
    }

    [HttpGet("by-customer/{customerId}")]
    public async Task<IActionResult> GetRentalsByCustomerId(int customerId)
    {
        var response = await rentalService.GetRentalsByCustomerIdAsync(customerId);
        if (!response.IsSucces)
            return StatusCode(response.StatusCode, response.Message);

        return Ok(response.Data);
    }

    [HttpGet("by-car/{carId}")]
    public async Task<IActionResult> GetRentalsByCarId(int carId)
    {
        var response = await rentalService.GetRentalsByCarIdAsync(carId);
        if (!response.IsSucces)
            return StatusCode(response.StatusCode, response.Message);

        return Ok(response.Data);
    }

    [HttpGet("revenue")]
    public async Task<IActionResult> GetTotalRevenue([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var response = await rentalService.GetTotalRevenueAsync(startDate, endDate);
        if (!response.IsSucces)
            return StatusCode(response.StatusCode, response.Message);

        return Ok(response.Data);
    }

    [HttpGet("car-usage")]
    public async Task<IActionResult> GetCarUsage([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var response = await rentalService.GetCarUsageAsync(startDate, endDate);
        if (!response.IsSucces)
            return StatusCode(response.StatusCode, response.Message);

        return Ok(response.Data);
    }
}

