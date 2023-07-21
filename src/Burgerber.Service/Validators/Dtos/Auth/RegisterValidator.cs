using Burgerber.Service.Dtos.Auth;
using FluentValidation;

namespace Burgerber.Service.Validators.Dtos.Auth;

public class RegisterValidator:AbstractValidator<RegisterDto>
{
    public RegisterValidator()
    {
        RuleFor(dto => dto.First_Name).NotEmpty().NotNull().WithMessage("Ism bo'lishi shart!")
            .MaximumLength(30).WithMessage("Ism uzunligi 30 tadan oshib ketmasligi kerak!");
        RuleFor(dto => dto.First_Name).NotEmpty().NotNull().WithMessage("Ism bo'lishi shart!")
            .MaximumLength(30).WithMessage("Familiya uzunligi 30 tadan oshib ketmasligi kerak!");

        RuleFor(dto => dto.PhoneNumber).Must(phone => PhoneNumberValidator.isValid(phone))
            .WithMessage("Telefon raqam to'g'ri bo'lishi shart! Misol uchun : +998xxAAABBCC");

        RuleFor(dto => dto.Password).Must(password => PasswordValidator.isStrongPassword(password).isValid)
            .WithMessage("Password kuchli bo'lishi shart");
            
    }
}
