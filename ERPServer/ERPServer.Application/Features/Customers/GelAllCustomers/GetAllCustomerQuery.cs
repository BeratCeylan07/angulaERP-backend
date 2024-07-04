using ERPServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Customers.GelAllCustomers;

public sealed record GetAllCustomerQuery() : IRequest<Result<List<Customer>>>;