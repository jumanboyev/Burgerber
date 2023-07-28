using Burgerber.Service.Commons.Helpers;
using Burgerber.Service.Dtos.Products;
using FluentValidation;

namespace Burgerber.Service.Validators.Dtos.Products;

public class ProductCreateValidator:AbstractValidator<ProductCreateDto>
{
    public ProductCreateValidator()
    {
        RuleFor(dto => dto.Name).NotNull().NotEmpty().WithMessage("Name field is required!")
            .MinimumLength(2).WithMessage("Name must be more than 3 characters")
            .MaximumLength(50).WithMessage("Name must be less than 50 characters");

        RuleFor(dto => dto.UnitPrice).NotEmpty().NotNull().WithMessage("Price field is required!")
            .GreaterThanOrEqualTo(0).WithMessage("Yaxshimisiz. To'y qachon to'y");

        int maxImageSizeMB = 3;
        RuleFor(dto => dto.Image).NotEmpty().NotNull().WithMessage("Image field is required");
        RuleFor(dto => dto.Image.Length).LessThan(maxImageSizeMB * 1024 * 1024 + 1).WithMessage($"Image size must be less than {maxImageSizeMB} MB");
        RuleFor(dto => dto.Image.FileName).Must(predicate =>
        {
            FileInfo fileInfo = new FileInfo(predicate);
            return MediaHelper.GetImageExtensions().Contains(fileInfo.Extension);
        }).WithMessage("This file type is not image file");
    }
}
