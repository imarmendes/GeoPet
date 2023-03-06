using AutoMapper;
using GeoPet.DataContract.Model;
using GeoPet.DataContract.Request;
using GeoPet.DataContract.Response;
using GeoPet.Interfaces.Repository;
using GeoPet.Interfaces.Services;
using GeoPet.Validation;
using GeoPet.Validation.Base;

namespace GeoPet.Service;

public class PetService : IPetService
{
    private readonly IPetRepository _petRepository;
    private readonly IMapper _mapper;

    private readonly IHttpContextAccessor _httpContextAcessor;


    public PetService(IPetRepository petRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _petRepository = petRepository;
        _mapper = mapper;
        _httpContextAcessor = httpContextAccessor;

    }

    private bool ValidateAuthorization(int id)
    {
        var claim = int.Parse(_httpContextAcessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value!);

        if (claim != 1 && claim != id) return true;

        return false;
    }
    public async Task<Response> CreatePet(PetRequest petRequest)
    {
        try
        {
            var petValidation = new PetValidate();
            var petIsValid = petValidation.Validate(petRequest);

            var errors = GetValidations.GetErrors(petIsValid);

            if (errors.Report.Any())
                return errors;
            var pet = _mapper.Map<Pet>(petRequest);

            if (ValidateAuthorization(pet.OwnerId)) return Response.Unprocessable(Report.Create("Sem permissão"));

            var petAdd = await _petRepository.Add(pet);

            var petResponse = _mapper.Map<PetResponse>(petAdd);

            var response = new Response<PetResponse>(petResponse);
            return response;
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create(e.Message));
        }
    }

    public async Task<Response> GetAllPets()
    {
        try
        {
            var pets = await _petRepository.GetAll();
            var petResponseList = _mapper.Map<List<Pet>, List<PetResponse>>(pets);
            var response = new Response<List<PetResponse>>(petResponseList);
            return response;
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create(e.Message));
        }
    }

    public async Task<Response> GetPetById(Guid id)
    {
        try
        {
            var pet = await _petRepository.GetById(id);

            if (ValidateAuthorization(pet.OwnerId)) return Response.Unprocessable(Report.Create("Sem permissão"));

            var petResponse = _mapper.Map<PetResponse>(pet);
            var response = new Response<PetResponse>(petResponse);
            return response;
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create(e.Message));
        }
    }

    public async Task<Response> UpdatePet(Guid id, PetRequest petRequest)
    {
        try
        {
            var petValidation = new PetValidate();
            var petIsValid = petValidation.Validate(petRequest);

            var errors = GetValidations.GetErrors(petIsValid);

            if (errors.Report.Any())
                return errors;

            var pet = _mapper.Map<Pet>(petRequest);
            pet.Id = id;

            if (ValidateAuthorization(pet.OwnerId)) return Response.Unprocessable(Report.Create("Sem permissão"));

            var petUpdated = await _petRepository.Update(id, pet);

            var petResponse = _mapper.Map<PetResponse>(petUpdated);

            var response = new Response<PetResponse>(petResponse);
            return response;
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create(e.Message));
        }
    }

    public async Task<Response> DeletePet(Guid id)
    {
        try
        {
            var petToDelete = await _petRepository.GetById(id);

            if (ValidateAuthorization(petToDelete.OwnerId)) return Response.Unprocessable(Report.Create("Sem permissão"));

            var petDeleted = await _petRepository.Remove(petToDelete);
            var petResponse = _mapper.Map<PetResponse>(petDeleted);

            var response = new Response<PetResponse>(petResponse);
            return response;
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create(e.Message));
        }
    }
}