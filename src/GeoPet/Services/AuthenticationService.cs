using AutoMapper;
using GeoPet.DataContract.Request;
using GeoPet.DataContract.Response;
using GeoPet.Interfaces.Repository;
using GeoPet.Interfaces.Services;
using GeoPet.Validation;
using GeoPet.Validation.Base;


namespace GeoPet.Service;

public class AuthenticationService : IAuthenticationService
{
    private readonly IOwnerRepository _ownerRepository;
    private readonly ISecurityServices _securityServices;
    private readonly IJwtService _jwtService;

    public AuthenticationService(
        IOwnerRepository ownerRepository,
        ISecurityServices securityServices,
        IJwtService jwtService
    )
    {
        _ownerRepository = ownerRepository;
        _securityServices = securityServices;
        _jwtService = jwtService;
    }

    public async Task<Response> Authenticate(UserRequest user)
    {
        try
        {
            var userValidation = new UserValidate();
            var userIsValid = userValidation.Validate(user);

            var errors = GetValidations.GetErrors(userIsValid);
            if (errors.Report.Any()) return errors;

            var owner = await _ownerRepository.GetByEmail(user.Email);

            var passwordIsValid = await _securityServices.VerifyPassword(owner.Password, user);
            if (!passwordIsValid.Data) return Response.Unprocessable(Report.Create("Email ou senha são inválidos"));

            var token = _jwtService.GenerateToken(owner);

            var response = new Response<string>(token);
            return response;
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create(e.Message));
        }
    }
}