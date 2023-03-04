using System.Drawing;
using GeoPet.DataContract.Response;
using GeoPet.Interfaces.Services;
using GeoPet.Validation.Base;
using SkiaSharp;
using SkiaSharp.QrCode;
using SkiaSharp.QrCode.Image;

namespace GeoPet.Service;

public class QRCodeService : IQRCodeService
{
    public byte[] Generate(string payload)
    {
        using var generetor = new QRCodeGenerator();

        var qrcode = generetor.CreateQrCode(payload, ECCLevel.H);

        var info = new SKImageInfo(512, 512);
        using var surface = SKSurface.Create(info);

        var canvas = surface.Canvas;
        canvas.Render(qrcode, 512, 512);

        using var image = surface.Snapshot();
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);

        return data.ToArray();
    }

    public byte[] GetLostPet(PetResponse pet)
    {
        var responseToString = $"Nome: {pet.Name}\nIdade: {pet.Age}\nRaca: {pet.Breed}\nTamanho: {pet.Size}";

        var qrcode = Generate(responseToString);

        return qrcode;
    }
}