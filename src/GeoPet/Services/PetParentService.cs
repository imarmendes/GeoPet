using AutoMapper;
using GeoPet.DataContract.Model;
using GeoPet.DataContract.Request;
using GeoPet.DataContract.Response;
using GeoPet.Interfaces.Repository;
using GeoPet.Interfaces.Services;
using GeoPet.Validation;
using GeoPet.Validation.Base;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace GeoPet.Service;

public class PetParentService : IUserService
{
    private readonly IUserRepository _petParentRepository;
    private readonly IMapper _mapper;
    private readonly ISecurityServices _securityServices;
    private readonly ICepService _cepService;

<<<<<<< Updated upstream

    public PetParentService(IUserRepository petParentRepository, IMapper mapper, ISecurityServices securityServices, ICepService cepService)
=======
    public PetParentService(IUserRepository petParentRepository, IMapper mapper, ISecurityServices securityServices)
>>>>>>> Stashed changes
    {
        _petParentRepository = petParentRepository;
        _mapper = mapper;
        _securityServices = securityServices;
        _cepService = cepService;
    }

<<<<<<< Updated upstream
    public async Task<Response> CreateUser(UserRequest userRequest)
    {
        try
        {
            var petParentValidation = new PetParentValidate(_cepService);
            var petIsValid = petParentValidation.Validate(userRequest);
=======
    public async Task<Response> CreateUser(UserRequest petParentRequest)
    {
        try
        {
            var petParentValidation = new PetParentValidate();
            var petIsValid = petParentValidation.Validate(petParentRequest);
>>>>>>> Stashed changes

            var errors = GetValidations.GetErrors(petIsValid);

            if (errors.Report.Any())
                return errors;

            // var isEquals = await _securityServices.ComparePassword(petParentRequest.Password, petParentRequest.ConfirmPassword);

            // if (!isEquals.Data)
            //     return Response.Unprocessable(Report.Create("Os password não são iguais."));

            var passwordEncripted = await _securityServices.EncryptPassword(userRequest.Password);

            userRequest.Password = passwordEncripted.Data;

<<<<<<< Updated upstream
            var petParent = _mapper.Map<User>(userRequest);
=======
            var petParent = _mapper.Map<User>(petParentRequest);
>>>>>>> Stashed changes

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
            var petParentValidation = new PetParentValidate(_cepService);
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