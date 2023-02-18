using GeoPet.DataContract.Request;
using GeoPet.Validation.Base;

namespace GeoPet.Interfaces.Services;

public interface IPetService
{
    Task<Response> CreatePet(PetRequest petRequest);
    Task<Response> GetAllPets();
    Task<Response> GetPetById(int id);
    Task<Response> UpdatePet(int id, PetRequest petRequest);
    Task<Response> DeletePet(int id);
}