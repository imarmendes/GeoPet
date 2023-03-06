using FluentValidation;
using GeoPet.DataContract.Request;

namespace GeoPet.Validation;

public class PetValidate : AbstractValidator<PetRequest>
{
    private List<string> Sizes { get; set; } = new List<string>() { "pequeno", "médio", "grande" };

    public PetValidate()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("Nome do pet é obrigatório.");
        RuleFor(p => p.Age)
            .NotEmpty()
            .WithMessage("Idade do pet é obrigatório.");
        RuleFor(p => p.Size)
            .NotEmpty()
            .WithMessage("Porte é obrigatório")
            .Must(size => Sizes.Contains(size))
            .WithMessage("Tamanho do porte é inválido");
        RuleFor(p => p.Breed)
            .NotEmpty()
            .WithMessage("Raça do pet é obrigatório.");
    }
}