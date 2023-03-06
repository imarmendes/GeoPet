using GeoPet.DataContract.Request;
using GeoPet.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace GeoPet.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PetController : ControllerBase
{
    private readonly IPetService _petService;

    public PetController(IPetService petService)
    {
        _petService = petService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePet([FromBody] PetRequest petRequest)
    {
        var response = await _petService.CreatePet(petRequest);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPets()
    {
        var response = await _petService.GetAllPets();
        return Ok(response);
    }

    [HttpGet("{petId}")]
    public async Task<IActionResult> GetPetById(Guid petId)
    {
        var response = await _petService.GetPetById(petId);
        return Ok(response);
    }

    [HttpPut("{petId}")]
    public async Task<IActionResult> UpdatePet(Guid petId, [FromBody] PetRequest petRequest)
    {
        var response = await _petService.UpdatePet(petId, petRequest);
        return Ok(response);
    }

    [HttpDelete("{petId}")]
    public async Task<IActionResult> DeletePet(Guid petId)
    {
        var response = await _petService.DeletePet(petId);
        return Ok(response);
    }
}