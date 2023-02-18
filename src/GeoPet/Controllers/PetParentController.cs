using GeoPet.DataContract.Request;
using GeoPet.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeoPet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PetParentController  : ControllerBase
{
    private readonly IPetParentService _petParentService;

    public PetParentController(IPetParentService petParentService)
    {
        _petParentService = petParentService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePetParent([FromBody] PetParentRequest petParentRequest)
    {
        var response = await _petParentService.CreatePetParent(petParentRequest);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPetParents()
    {
        var response = await _petParentService.GetAllPetParents();
        return Ok(response);
    }

    [HttpGet("/byId/{parentId}")]
    public async Task<IActionResult> GetPetParentById(int parentId)
    {
        var response = await _petParentService.GetPetParentById(parentId);
        return Ok(response);
    }

    [HttpPut("/{parentId}")]
    public async Task<IActionResult> UpdatePetParent(int parentId, [FromBody] PetParentRequest petParentRequest)
    {
        var response = await _petParentService.UpdatePetParent(parentId, petParentRequest);
        return Ok(response);
    }

    [HttpDelete("/{parentId}")]
    public async Task<IActionResult> DeletePetParent(int parentId)
    {
        var response = await _petParentService.DeletePetParent(parentId);
        return Ok(response);
    }
}