using System.ComponentModel.DataAnnotations;

namespace GeoPet.DataContract.Model;

public class Pet
{
    public Guid PetId { get; set; }
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public string Size { get; set; } = null!;
    public string Breed { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}