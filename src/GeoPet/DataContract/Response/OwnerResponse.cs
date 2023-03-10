using GeoPet.DataContract.Model;

namespace GeoPet.DataContract.Response;

public class OwnerResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string CEP { get; set; } = null!;
    public List<Pet>? Pets { get; set; }

}