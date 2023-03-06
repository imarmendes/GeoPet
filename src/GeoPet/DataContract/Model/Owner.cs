using System.ComponentModel.DataAnnotations;

namespace GeoPet.DataContract.Model;

public class Owner
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string CEP { get; set; } = null!;

    public List<Pet>? Pets { get; set; }
}