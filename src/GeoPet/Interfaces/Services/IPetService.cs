using GeoPet.DataContract.Request;
using GeoPet.Validation.Base;

namespace GeoPet.Interfaces.Services;

public interface IPetService
{
    Task<Response> CreatePet(PetRequest petRequest);
    Task<Response> GetAllPets();
    Task<Response> GetPetById(Guid id);
    Task<Response> UpdatePet(Guid id, PetRequest petRequest);
    Task<Response> DeletePet(Guid id);
}