using Domain.ApiResponse;
using Domain.DTOs.Car;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CarController(ICarService carService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateCar([FromBody] CreateCarDto createCarDto)
    {
        var response = await carService.CreateCarAsync(createCarDto);

        if (!response.IsSucces)
            return StatusCode(response.StatusCode, response.Message);

        return Ok(response.Message);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCarById(int id)
    {
        var response = await carService.GetCarByIdAsync(id);

        if (!response.IsSucces)
            return StatusCode(response.StatusCode, response.Message);

        return Ok(response.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetCars()
    {
        var response = await carService.GetCarsAsync();

        if (!response.IsSucces)
            return StatusCode(response.StatusCode, response.Message);

        return Ok(response.Data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCar(int id, [FromBody] UpdateCarDto updateCarDto)
    {
        var response = await carService.UpdateCarAsync(id, updateCarDto);

        if (!response.IsSucces)
            return StatusCode(response.StatusCode, response.Message);

        return Ok(response.Message);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCar(int id)
    {
        var response = await carService.DeleteCarAsync(id);

        if (!response.IsSucces)
            return StatusCode(response.StatusCode, response.Message);

        return Ok(response.Message);
    }
}

