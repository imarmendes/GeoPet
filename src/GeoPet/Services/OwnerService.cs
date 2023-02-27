using AutoMapper;
using GeoPet.DataContract.Request;
using GeoPet.DataContract.Response;
using GeoPet.DataContract.Model;
using GeoPet.Interfaces.Repository;
using GeoPet.Interfaces.Services;
using GeoPet.Validation;
using GeoPet.Validation.Base;


namespace GeoPet.Service;

public class OwnerService : IOwnerService
{
    private readonly IOwnerRepository _petParentRepository;
    private readonly IMapper _mapper;
    private readonly ISecurityServices _securityServices;
    private readonly ICepService _cepService;
    private readonly IHttpContextAccessor _httpContextAcessor;


    public OwnerService(IOwnerRepository petParentRepository, IMapper mapper, ISecurityServices securityServices, ICepService cepService, IHttpContextAccessor httpContextAccessor)
    {
        _petParentRepository = petParentRepository;
        _mapper = mapper;
        _securityServices = securityServices;
        _cepService = cepService;
        _httpContextAcessor = httpContextAccessor;
    }

    private bool ValidateAuthorization(int id)
    {
        var claim = int.Parse(_httpContextAcessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value!);

        if (claim != 1 && claim != id) return true;

        return false;
    }

    public async Task<Response> CreateUser(OwnerRequest userRequest)
    {
        try
        {
            var petParentValidation = new OwnerValidate(_cepService);
            var petIsValid = petParentValidation.Validate(userRequest);

            var errors = GetValidations.GetErrors(petIsValid);

            if (errors.Report.Any())
                return errors;

            var passwordEncripted = await _securityServices.EncryptPassword(userRequest.Password);

            userRequest.Password = passwordEncripted.Data!;

            var petParent = _mapper.Map<DataContract.Model.Owner>(userRequest);

            var petParentAdd = _petParentRepository.Add(petParent);

            var petParentResponse = _mapper.Map<UserResponse>(petParentAdd.Result);

            var response = new Response<UserResponse>(petParentResponse);
            return response;
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create(e.Message));
        }
    }

    public async Task<Response> GetAllUsers()
    {
        try
        {
            var petParents = await _petParentRepository.GetAll();
            var petParentResponseList = _mapper.Map<List<DataContract.Model.Owner>, List<UserResponse>>(petParents);
            var response = new Response<List<UserResponse>>(petParentResponseList);
            return response;
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create(e.Message));
        }
    }

    public async Task<Response> GetUserById(int id)
    {
        try
        {
            if (ValidateAuthorization(id)) return Response.Unprocessable(Report.Create("Sem permissão"));

            var petParent = await _petParentRepository.GetById(id);
            var petParentResponse = _mapper.Map<UserResponse>(petParent);
            var response = new Response<UserResponse>(petParentResponse);
            return response;
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create(e.Message));
        }
    }

    public async Task<Response> UpdateUser(int id, OwnerRequest ownerRequest)
    {
        try
        {
            if (ValidateAuthorization(id)) return Response.Unprocessable(Report.Create("Sem permissão"));

            var petParentValidation = new OwnerValidate(_cepService);
            var petIsValid = petParentValidation.Validate(ownerRequest);

            var errors = GetValidations.GetErrors(petIsValid);

            if (errors.Report.Any())
                return errors;

            var passwordEncripted = await _securityServices.EncryptPassword(ownerRequest.Password);
            ownerRequest.Password = passwordEncripted.Data!;

            var petParent = _mapper.Map<Owner>(ownerRequest);
            petParent.Id = id;

            var petParentUpdated = await _petParentRepository.Update(id, petParent);

            var petParentResponse = _mapper.Map<UserResponse>(petParentUpdated);

            var response = new Response<UserResponse>(petParentResponse);
            return response;
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create(e.Message));
        }
    }

    public async Task<Response> DeleteUser(int id)
    {
        try
        {
            if (ValidateAuthorization(id)) return Response.Unprocessable(Report.Create("Sem permissão"));

            var petParentToDelete = _petParentRepository.GetById(id);

            var petParentDeleted = await _petParentRepository.Remove(petParentToDelete.Result);
            var petParentResponse = _mapper.Map<UserResponse>(petParentDeleted);

            var response = new Response<UserResponse>(petParentResponse);
            return response;
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create(e.Message));
        }
    }
}