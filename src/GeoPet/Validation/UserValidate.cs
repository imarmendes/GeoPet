using FluentValidation;
using GeoPet.DataContract.Request;

namespace GeoPet.Validation;

public class UserValidate : AbstractValidator<UserRequest>
{
    public UserValidate()
    {
        RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage("O e-mail é obrigatório.")
            .EmailAddress()
            .WithMessage("O e-mail deve seguir o padrão email@email.com");

        RuleFor(u => u.Password)
            .NotEmpty()
            .WithMessage("A senha é obrigatória.");
    }
}