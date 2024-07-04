using FluentValidation;

namespace ERPServer.Application.Features.Customers.CreateCustomer;

public sealed class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        // With message ile özel bir hata döndürebilirsiniz......
        // RuleFor(p => p.TaxNumber).MinimumLength(10).WithMessage("").MaximumLength(11);
        // HOCAMIZA SORDUĞUM SORUYU ŞU ŞEKİLDE CEVAPLADI BU 3 DOSYAYA AYIRDIĞIMIZDA HANDLER VALİDATOR BİRBİLERİNİ OTOMATİK NASIL BULUYORLAR? MediaTR kütüphaneis otomatik eşleştiriyor
        RuleFor(p => p.TaxNumber).MinimumLength(10).MaximumLength(11);
        RuleFor(p => p.Name).MinimumLength(3);
    }
}