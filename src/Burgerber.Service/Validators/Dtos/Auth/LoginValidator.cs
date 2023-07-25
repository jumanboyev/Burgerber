using Burgerber.Service.Dtos.Auth;
using FluentValidation;

namespace Burgerber.Service.Validators.Dtos.Auth;

public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(dto => dto.PhoneNumber).Must(phone => PhoneNumberValidator.isValid(phone))
            .WithMessage("Telefon raqam to'g'ri bo'lishi shart! Misol uchun : +998xxAAABBCC");

        RuleFor(dto => dto.Password).Must(password => PasswordValidator.isStrongPassword(password).isValid)
            .WithMessage("Password kuchli bo'lishi shart");
    }
}
