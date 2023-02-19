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

    public PetService(IPetRepository petRepository, IMapper mapper)
    {
        _petRepository = petRepository;
        _mapper = mapper;
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

    public async Task<Response> GetPetById(int id)
    {
        try
        {
            var pet = await _petRepository.GetById(id);
            var petResponse = _mapper.Map<PetResponse>(pet);
            var response = new Response<PetResponse>(petResponse);
            return response;
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create(e.Message));
        }
    }

    public async Task<Response> UpdatePet(int id, PetRequest petRequest)
    {
        try
        {
            var petValidation = new PetValidate();
            var petIsValid = petValidation.Validate(petRequest);

            var errors = GetValidations.GetErrors(petIsValid);

            if (errors.Report.Any())
                return errors;

            var pet = _mapper.Map<Pet>(petRequest);

            var petUpdated = _petRepository.Update(pet);

            var petResponse = _mapper.Map<PetResponse>(petUpdated.Result);

            var response = new Response<PetResponse>(petResponse);
            return response;
        }
        catch (Exception e)
        {
            return Response.Unprocessable(Report.Create(e.Message));
        }
    }

    public async Task<Response> DeletePet(int id)
    {
        try
        {
            var petToDelete = _petRepository.GetById(id);

            var petDeleted = await _petRepository.Remove(petToDelete.Result);
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