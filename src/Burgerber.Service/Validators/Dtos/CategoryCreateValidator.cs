using Burgerber.Service.Dtos.Categories;
using FluentValidation;

namespace Burgerber.Service.Validators.Dtos;

public class CategoryCreateValidator:AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateValidator()
    {
        RuleFor(dto => dto.Name).NotEmpty().NotNull().WithMessage("Ism bo'sh va Null bo'lishi mumkin emas")
                                .MaximumLength(50).WithMessage("Maxsimum ism uzunligi 50 dan oshmasligi kerak")
                                .MinimumLength(3).WithMessage("Minimum ism uzunligi 3 dan kam bo'lmasligi kerak");

        RuleFor(dto=> dto.Description).NotNull().NotEmpty().WithMessage("Description bo'sh va Null bo'lishi mumkin emas")
                                .MinimumLength(15).WithMessage("Minimum description uzunligi 15 dan kam bo'lmasligi kerak");
    }
}
