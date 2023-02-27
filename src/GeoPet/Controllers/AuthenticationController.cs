using GeoPet.DataContract.Request;
using GeoPet.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeoPet.Controllers;

[ApiController]
[Route("/api/login")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Authenticate([FromBody] UserRequest user)
    {
        var response = await _authenticationService.Authenticate(user);

        return Ok(response);
    }
}