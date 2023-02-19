using AutoMapper;
using GeoPet.DataContract.Model;
using GeoPet.DataContract.Request;
using GeoPet.DataContract.Response;
using GeoPet.Interfaces.Repository;
using GeoPet.Interfaces.Services;
using GeoPet.Validation;
using GeoPet.Validation.Base;

namespace GeoPet.Service;

public class PetParentService : IUserService
{
    private readonly IUserRepository _petParentRepository;
    private readonly IMapper _mapper;
    private readonly ISecurityServices _securityServices;

    public PetParentService(IUserRepository petParentRepository, IMapper mapper, ISecurityServices securityServices)
    {
        _petParentRepository = petParentRepository;
        _mapper = mapper;
        _securityServices = securityServices;
    }

    public async Task<Response> CreateUser(UserRequest petParentRequest)
    {
        try
        {
            var petParentValidation = new PetParentValidate();
            var petIsValid = petParentValidation.Validate(petParentRequest);

            var errors = GetValidations.GetErrors(petIsValid);

            if (errors.Report.Any())
                return errors;

            // var isEquals = await _securityServices.ComparePassword(petParentRequest.Password, petParentRequest.ConfirmPassword);

            // if (!isEquals.Data)
            //     return Response.Unprocessable(Report.Create("Os password não são iguais."));

            var passwordEncripted = await _securityServices.EncryptPassword(petParentRequest.Password);

            petParentRequest.Password = passwordEncripted.Data;

            var petParent = _mapper.Map<User>(petParentRequest);

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
            var petParentResponseList = _mapper.Map<List<User>, List<UserResponse>>(petParents);
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

    public async Task<Response> UpdateUser(int id, UserRequest petParentRequest)
    {
        try
        {
            var petParentValidation = new PetParentValidate();
            var petIsValid = petParentValidation.Validate(petParentRequest);

            var errors = GetValidations.GetErrors(petIsValid);

            if (errors.Report.Any())
                return errors;

            var petParent = _mapper.Map<User>(petParentRequest);

            var petParentUpdated = _petParentRepository.Update(petParent);

            var petParentResponse = _mapper.Map<UserResponse>(petParentUpdated.Result);

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