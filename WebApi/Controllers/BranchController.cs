using Domain.DTOs.Branch;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BranchController(IBranchService branchService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateBranch(CreateBranchDto createBranchDto)
    {
        var response = await branchService.CreateBranchAsync(createBranchDto);

        if (!response.IsSucces)
            return StatusCode(response.StatusCode, new { error = response.Message });

        return Ok(new { message = response.Message });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBranchById(int id)
    {
        var response = await branchService.GetBranchByIdAsync(id);

        if (!response.IsSucces)
            return StatusCode(response.StatusCode, new { error = response.Message });

        return Ok(response.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetBranches()
    {
        var response = await branchService.GetBranchesAsync();

        if (!response.IsSucces)
            return StatusCode(response.StatusCode, new { error = response.Message });

        return Ok(response.Data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBranch(int id, UpdateBranchDto updateBranchDto)
    {
        var response = await branchService.UpdateBranchAsync(id, updateBranchDto);

        if (!response.IsSucces)
            return StatusCode(response.StatusCode, new { error = response.Message });

        return Ok(new { message = response.Message });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBranch(int id)
    {
        var response = await branchService.DeleteBranchAsync(id);

        if (!response.IsSucces)
            return StatusCode(response.StatusCode, new { error = response.Message });

        return Ok(new { message = response.Message });
    }
}
