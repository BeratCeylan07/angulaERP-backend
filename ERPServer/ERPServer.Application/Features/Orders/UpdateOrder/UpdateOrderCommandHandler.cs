using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Respositories;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Orders.UpdateOrder;

internal sealed class UpdateOrderCommandHandler(
    IOrderRepository orderRepository,
    IOrderDetailRepository orderDetailRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateOrderCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        Order? order = await orderRepository.Where(p => p.Id == request.Id)
            .Include(i => i.Details)
            .FirstOrDefaultAsync(cancellationToken);
        if (order is null)
        {
            return Result<string>.Failure("Sipariş Bulunamadı");
        }
            
        // Güncellemek istediğimiz kayıttaki mevcut details ları sildikten sonra request ile gönderdiğimiz detailsları tekrardan ekliyoruz..... burası zor ve önemli
        orderDetailRepository.DeleteRange(order.Details);
        mapper.Map(request, order);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Sipariş başarıyla güncellendi";

    }
}