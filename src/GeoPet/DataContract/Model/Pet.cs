using System.ComponentModel.DataAnnotations;

namespace GeoPet.DataContract.Model;

public class Pet
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public string Size { get; set; } = null!;
    public string Breed { get; set; } = null!;

    public int OwnerId { get; set; }
    public Owner Owner { get; set; } = null!;
}