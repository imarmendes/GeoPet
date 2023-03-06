using GeoPet.DataContract.Request;
using GeoPet.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GeoPet.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OwnerController : ControllerBase
{
    private readonly IOwnerService _ownerService;

    public OwnerController(IOwnerService ownerService)
    {
        _ownerService = ownerService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateOwner([FromBody] OwnerRequest ownerRequest)
    {
        var response = await _ownerService.CreateUser(ownerRequest);
        return Ok(response);
    }

    [HttpGet]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> GetAllOwners()
    {
        var response = await _ownerService.GetAllUsers();
        return Ok(response);
    }

    [HttpGet("{ownerId}")]
    public async Task<IActionResult> GetOwnerById(int ownerId)
    {
        var response = await _ownerService.GetUserById(ownerId);
        return Ok(response);
    }

    [HttpPut("{ownerId}")]
    public async Task<IActionResult> UpdateOwner(int ownerId, [FromBody] OwnerRequest ownerRequest)
    {
        var response = await _ownerService.UpdateUser(ownerId, ownerRequest);
        return Ok(response);
    }

    [HttpDelete("{ownerId}")]
    public async Task<IActionResult> DeleteOwner(int ownerId)
    {
        var response = await _ownerService.DeleteUser(ownerId);
        return Ok(response);
    }
}