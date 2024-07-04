using ERPServer.Domain.Entities;
using ERPServer.Domain.Respositories;
using ERPServer.Infrastructure.Context;
using GenericRepository;

namespace ERPServer.Infrastructure.Repositories;

internal class ProductRepository : Repository<Product, ApplicationDbContext>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }
}