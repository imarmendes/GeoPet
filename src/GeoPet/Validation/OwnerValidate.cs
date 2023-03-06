using FluentValidation;
using GeoPet.DataContract.Request;
using GeoPet.Interfaces.Services;
using GeoPet.Validation.Base;

namespace GeoPet.Validation;

public class OwnerValidate : AbstractValidator<OwnerRequest>
{
    private readonly ICepService _cepService;

    public OwnerValidate(ICepService cepService)
    {
        _cepService = cepService;

        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("O nome é obrigatório.")
            .MinimumLength(3)
            .WithMessage("O nome tem que ter o tamanho minimo de 3 caracteres.");

        RuleFor(p => p.Email)
            .NotEmpty()
            .WithMessage("O e-mail é obrigatório.")
            .EmailAddress()
            .WithMessage("O e-mail deve seguir o padrão email@email.com");

        RuleFor(p => p.Password)
            .NotEmpty()
            .WithMessage("A senha é obrigatória.")
            .MinimumLength(8)
            .WithMessage("A senha deve ter no mínimo 8 caracteres.");

        RuleFor(p => p.CEP)
            .NotEmpty()
            .WithMessage("CEP")
            .Length(8, 9)
            .WithMessage("Formato do CEP inválido")
            .Must(cep => ValidateCep(cep).Result)
            .WithMessage("CEP não existe");

    }

    private async Task<bool> ValidateCep(string CEP)
    {
        var cep = await _cepService.GetCep(CEP) as Response<object>;

        if (cep?.Data is null) return false;

        return true;
    }
}