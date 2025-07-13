using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticController(IAnalyticsService analyticsService) : ControllerBase
{
    [HttpGet("total-revenue")]
    public async Task<IActionResult> GetTotalRevenueAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var revenue = await analyticsService.GetTotalRevenueAsync(startDate, endDate);
        return Ok(new { totalRevenue = revenue });
    }

    [HttpGet("car-usage")]
    public async Task<IActionResult> GetCarUsageAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var usageStats = await analyticsService.GetCarUsageAsync(startDate, endDate);
        return Ok(usageStats);
    }

    [HttpGet("top-models")]
    public async Task<IActionResult> GetTop5PopularModelsAsync([FromQuery] int year, [FromQuery] int month)
    {
        var topModels = await analyticsService.GetTop5PopularModelsAsync(year, month);
        return Ok(topModels);
    }

    [HttpGet("top-customers")]
    public async Task<IActionResult> GetTopCustomersAsync([FromQuery] int count = 5)
    {
        var topCustomers = await analyticsService.GetTopCustomersAsync(count);
        return Ok(topCustomers);
    }
}
