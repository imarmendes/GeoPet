
using GeoPet.DataContract.Response;
using GeoPet.Validation.Base;
using SkiaSharp;

namespace GeoPet.Interfaces.Services;

public interface IQRCodeService
{
    public byte[] Generate(string data);
    public byte[] GetLostPet(PetResponse pet);
}