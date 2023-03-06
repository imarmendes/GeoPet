using GeoPet.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GeoPet.Validation.Base;
using GeoPet.DataContract.Response;
using GeoPet.DataContract.Request;

namespace GeoPet.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NominatinController : ControllerBase
{
    private readonly INominatinService _nominatinService;

    public NominatinController(INominatinService nominatinService)
    {
        _nominatinService = nominatinService;
    }

    [HttpGet()]
    public async Task<IActionResult> GetAdress(string lat, string lon)
    {
        var response = await _nominatinService.Reverse(lat, lon);

        return Ok(response);
    }


}