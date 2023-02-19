using GeoPet.DataContract.Model;

namespace GeoPet.DataContract.Response;

public class UserResponse
{
    public int UserId { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string CEP { get; set; } = null!;
    public List<Pet>? Pets { get; set; }

}