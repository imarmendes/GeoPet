namespace GeoPet.DataContract.Request;

public class UserRequest
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string CEP { get; set; } = null!;
}