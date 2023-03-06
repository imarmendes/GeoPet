using GeoPet.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GeoPet.Validation.Base;
using GeoPet.DataContract.Response;

namespace GeoPet.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class LostPetController : ControllerBase
{
    private readonly IPetService _petService;
    private readonly IQRCodeService _qrCodeService;

    public LostPetController(IPetService petService, IQRCodeService qRCodeService)
    {
        _petService = petService;
        _qrCodeService = qRCodeService;
    }



    [HttpGet("{petId}")]
    public async Task<IActionResult> GetPetById(Guid petId)
    {
        var response = await _petService.GetPetById(petId) as Response<PetResponse>;

        var image = _qrCodeService.GetLostPet(response!.Data!);

        return File(image, "image/jpeg");

    }


}