using ERPServer.Domain.Abstractions;
using ERPServer.Domain.Enums;

namespace ERPServer.Domain.Entities;

public sealed class Order : Entity
{
    public Guid CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public int OrderNumberYear { get; set; }
    public int OrderNumber { get; set; }
    public DateOnly CreatedAt { get; set; }
    public DateOnly DeliveryDate { get; set; }
    public OrderStatusEnum Status { get; set; } = OrderStatusEnum.Pending;
    public List<OrderDetail>? Details { get; set; }
}