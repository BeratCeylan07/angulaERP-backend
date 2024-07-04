using ERPServer.Domain.Entities;
using ERPServer.Domain.Respositories;
using ERPServer.Infrastructure.Context;
using GenericRepository;

namespace ERPServer.Infrastructure.Repositories;

internal class CustomerRepository : Repository<Customer, ApplicationDbContext>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context) : base(context)
    {
    }
}