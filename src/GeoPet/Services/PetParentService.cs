using AutoMapper;
using GeoPet.DataContract.Model;
using GeoPet.DataContract.Request;
using GeoPet.DataContract.Response;
using GeoPet.Interfaces.Repository;
using GeoPet.Interfaces.Services;
using GeoPet.Validation;
using GeoPet.Validation.Base;

namespace GeoPet.Service;

public class PetParentService : IPetParentService
{
    private readonly IPetParentRepository _petParentRepository;
    private readonly IMapper _mapper;
    private readonly ISecurityServices _securityServices;

    public PetParentService(IPetParentRepository petParentRepository, IMapper mapper, ISecurityServices securityServices)
    {
        _petParentRepository = petParentRepository;
        _mapper = mapper;
        _securityServices = securityServices;
    }

    public async Task<Response> CreatePetParent(PetParentRequest petParentRequest)
    {
        try
        {
            var petParentValidation = new PetParentValidate();
            var petIsValid = petParentValidation.Validate(petParentRequest);
            
            var errors = GetValidations.GetErrors(petIsValid);
            
            if (errors.Report.Any())
                return errors;
            
            var isEquals = await _securityServices.ComparePassword(petParentRequest.Password, petParentRequest.ConfirmPassword);

            if (!isEquals.Data)
                return Response.Unprocessable(Report.Create("Os password não são iguais."));

            var passwordEncripted = await _securityServices.EncryptPassword(petParentRequest.Password);

            petParentRequest.Password = passwordEncripted.Data;

            var petParent = _mapper.Map<PetParentDto>(petParentRequest);

            var petParentAdd = _petParentRepository.Add(petParent);
            
            var petParentResponse = _mapper.Map<PetParentResponse>(petParentAdd.Result);

            var response = new Response<PetParentResponse>(petParentResponse);
            return response;
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create(e.Message));
        }
    }

    public async Task<Response> GetAllPetParents()
    {
        try
        {
            var petParents = await _petParentRepository.GetAll();
            var petParentResponseList = _mapper.Map<List<PetParentDto>, List<PetParentResponse>>(petParents);
            var response = new Response<List<PetParentResponse>>(petParentResponseList);
            return response;
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create(e.Message));
        }
    }

    public async Task<Response> GetPetParentById(int id)
    {
        try
        {
            var petParent = await _petParentRepository.GetById(id);
            var petParentResponse = _mapper.Map<PetParentResponse>(petParent);
            var response = new Response<PetParentResponse>(petParentResponse);
            return response;
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create(e.Message));
        }
    }

    public async Task<Response> UpdatePetParent(int id, PetParentRequest petParentRequest)
    {
        try
        {
            var petParentValidation = new PetParentValidate();
            var petIsValid = petParentValidation.Validate(petParentRequest);
            
            var errors = GetValidations.GetErrors(petIsValid);
            
            if (errors.Report.Any())
                return errors;
            
            var petParent = _mapper.Map<PetParentDto>(petParentRequest);

            var petParentUpdated = _petParentRepository.Update(petParent);
            
            var petParentResponse = _mapper.Map<PetParentResponse>(petParentUpdated.Result);

            var response = new Response<PetParentResponse>(petParentResponse);
            return response;
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create(e.Message));
        }
    }

    public async Task<Response> DeletePetParent(int id)
    {
        try
        {
            var petParentToDelete = _petParentRepository.GetById(id);
            
            var petParentDeleted = await _petParentRepository.Remove(petParentToDelete.Result);
            var petParentResponse = _mapper.Map<PetParentResponse>(petParentDeleted);

            var response = new Response<PetParentResponse>(petParentResponse);
            return response;
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create(e.Message));
        }
    }
}