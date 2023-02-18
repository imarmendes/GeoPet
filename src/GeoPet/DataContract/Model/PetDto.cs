using System.ComponentModel.DataAnnotations;

namespace GeoPet.DataContract.Model;

public class PetDto
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string DogBreed { get; set; }
    public int Age { get; set; }
    
    public int PetPerantId { get; set; }
    public PetParentDto PetPerant { get; set; }
}