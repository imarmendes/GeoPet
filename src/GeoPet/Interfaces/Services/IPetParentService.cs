using GeoPet.DataContract.Request;
using GeoPet.Validation.Base;

namespace GeoPet.Interfaces.Services;

public interface IPetParentService
{
    Task<Response> CreatePetParent(PetParentRequest petParentRequest);
    Task<Response> GetAllPetParents();
    Task<Response> GetPetParentById(int id);
    Task<Response> UpdatePetParent(int id, PetParentRequest petParentRequest);
    Task<Response> DeletePetParent(int id);
}