using FluentValidation;

namespace ERPServer.Application.Features.Products.CreateProduct;

public sealed class CreataProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreataProductCommandValidator()
    {
        RuleFor(p => p.TypeValue).GreaterThan(0);
    }
}