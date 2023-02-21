using GeoPet.DataContract.Request;
using GeoPet.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeoPet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PetParentController : ControllerBase
{
    private readonly IUserService _petParentService;

    public PetParentController(IUserService petParentService)
    {
        _petParentService = petParentService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePetParent([FromBody] UserRequest petParentRequest)
    {
        var response = await _petParentService.CreateUser(petParentRequest);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPetParents()
    {
        var response = await _petParentService.GetAllUsers();
        return Ok(response);
    }

    [HttpGet("{parentId}")]
    public async Task<IActionResult> GetPetParentById(int parentId)
    {
        var response = await _petParentService.GetUserById(parentId);
        return Ok(response);
    }

<<<<<<< Updated upstream
    [HttpPut("{parentId}")]
=======
    [HttpPut("/{parentId}")]
>>>>>>> Stashed changes
    public async Task<IActionResult> UpdatePetParent(int parentId, [FromBody] UserRequest petParentRequest)
    {
        var response = await _petParentService.UpdateUser(parentId, petParentRequest);
        return Ok(response);
    }

    [HttpDelete("{parentId}")]
    public async Task<IActionResult> DeletePetParent(int parentId)
    {
        var response = await _petParentService.DeleteUser(parentId);
        return Ok(response);
    }
}