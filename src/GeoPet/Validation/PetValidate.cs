using FluentValidation;
using GeoPet.DataContract.Request;

namespace GeoPet.Validation;

public class PetValidate : AbstractValidator<PetRequest>
{
    public PetValidate()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("Nome do pet é obrigatório.");
        RuleFor(p => p.Age)
            .NotEmpty()
            .WithMessage("Idade do pet é obrigatório.");
        RuleFor(p => p.Breed)
            .NotEmpty()
            .WithMessage("Raça do pet é obrigatório.");
    }
}