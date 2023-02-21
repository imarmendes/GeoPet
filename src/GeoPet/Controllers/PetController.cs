using GeoPet.DataContract.Request;
using GeoPet.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeoPet.Controllers;

[ApiController]
[Route("api/[controller]")]
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

    [HttpGet("{petId:int}")]
    public async Task<IActionResult> GetPetById(int petId)
    {
        var response = await _petService.GetPetById(petId);
        return Ok(response);
    }

    [HttpPut("{petId:int}")]
    public async Task<IActionResult> UpdatePet(int petId, [FromBody] PetRequest petRequest)
    {
        var response = await _petService.UpdatePet(petId, petRequest);
        return Ok(response);
    }

    [HttpDelete("{petId:int}")]
    public async Task<IActionResult> DeletePet(int petId)
    {
        var response = await _petService.DeletePet(petId);
        return Ok(response);
    }
}