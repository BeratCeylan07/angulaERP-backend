using ERPServer.Domain.Entities;
using ERPServer.Domain.Respositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Orders.GetAllOrder;

internal sealed class GetAllOrderQueryHandler(
    IOrderRepository orderRepository) : IRequestHandler<GetAllOrderQuery, Result<List<Order>>>
{
    public async Task<Result<List<Order>>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
    {
        List<Order>? orders = await orderRepository.GetAll()
            .Include(p => p.Customer)
            .Include(p => p.Details!)
            .ThenInclude(d => d.Product)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync(cancellationToken);

        return orders;
    }
}