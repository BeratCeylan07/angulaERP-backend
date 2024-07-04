using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Respositories;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Orders.CreateOrder;

internal sealed class CreateOrderCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateOrderCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        Order? LastOrder = await orderRepository
            .Where(p => p.OrderNumberYear == request.CreatedDate.Year)
            .OrderByDescending(p => p.OrderNumber)
            .FirstOrDefaultAsync(cancellationToken);
        int lastOrderNumber = 0;
        if (LastOrder is not null)
        {
            lastOrderNumber = LastOrder.OrderNumber;
        }

        Order order = mapper.Map<Order>(request);
        order.OrderNumber = lastOrderNumber++;
        order.OrderNumberYear = request.CreatedDate.Year;
        await orderRepository.AddAsync(order, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Sipariş Oluşturuldu";
    }
}