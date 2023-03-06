namespace GeoPet.DataContract.Request;

public class OwnerRequest : UserRequest
{
    public string Name { get; set; } = null!;
    public string CEP { get; set; } = null!;
}