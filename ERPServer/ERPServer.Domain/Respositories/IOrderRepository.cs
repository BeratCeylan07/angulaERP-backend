using ERPServer.Domain.Entities;
using GenericRepository;

namespace ERPServer.Domain.Respositories;

public interface IOrderRepository : IRepository<Order>
{
    
}

public interface IOrderDetailRepository : IRepository<OrderDetail>
{
    
}