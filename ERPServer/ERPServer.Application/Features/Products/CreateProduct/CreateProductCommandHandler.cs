using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Respositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Products.CreateProduct;

internal sealed class CreateProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateProductCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        bool isNameExists = await productRepository.AnyAsync(p => p.Name == request.Name, cancellationToken);
        if (isNameExists)
        {
            return Result<string>.Failure("Ürün Adı Daha Öncede Kullanılmış");
        }

        Product producut = mapper.Map<Product>(request); // burada bizim eski yöntem olan var product = new product() { id: "", name: "", type:"" } mantığını otomatik oluşturuyor
        await productRepository.AddAsync(producut, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Ürün Başarıyla Oluşturuldu";
    }
}


// Product Type 0,1,2,3, gibi değerler gelebilir biz ProductTypeEnum
// 0 dan değil 1 den başlattığımız için validator atayarak değeri 0 göndermesini engellememiz gerekiyor..
// Bu kontrol class ismi => CreataProductCommandValidator