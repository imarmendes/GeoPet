namespace GeoPet.DataContract.Response;

public class PetResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public string Size { get; set; } = null!;
    public string Breed { get; set; } = null!;
    public int OwnerId { get; set; }

}