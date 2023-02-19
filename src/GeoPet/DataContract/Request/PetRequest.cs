namespace GeoPet.DataContract.Request;

public class PetRequest
{
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public string Size { get; set; } = null!;
    public string Breed { get; set; } = null!;
    public int UserId { get; set; }
}