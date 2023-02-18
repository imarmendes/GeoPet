using GeoPet.DataContract.Model;
using GeoPet.Validation.Base;

namespace GeoPet.Interfaces.Services;

public interface ISecurityServices
{
    public Task<Response<bool>> ComparePassword(string password, string confirmPassword);
    public Task<Response<string>> EncryptPassword(string password);
    public Task<Response<bool>> VerifyPassword(string password, PetParentDto petParent);
}