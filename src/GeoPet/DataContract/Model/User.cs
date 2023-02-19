using System.ComponentModel.DataAnnotations;

namespace GeoPet.DataContract.Model;

public class User
{
    public int UserId { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string CEP { get; set; } = null!;

    public List<Pet>? Pets { get; set; }
}