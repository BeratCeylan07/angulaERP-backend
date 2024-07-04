using ERPServer.Domain.Entities;
using ERPServer.Domain.Respositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Products.GetAllProduct;

internal sealed class GetAllProductQueryHandler(
    IProductRepository productRepository) : IRequestHandler<GetAllProductQuery, Result<List<Product>>>
{
    public async Task<Result<List<Product>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        List<Product> products = await productRepository.GetAll().OrderBy(o => o.Name).ToListAsync(cancellationToken);

        return products;
    }
}