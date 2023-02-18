using System.ComponentModel.DataAnnotations;

namespace GeoPet.DataContract.Model;

public class PetParentDto : EntityBase
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public List<PetDto>? PetDtos { get; set; }
}