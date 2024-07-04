using ERPServer.Domain.Entities;
using ERPServer.Domain.Respositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Customers.GelAllCustomers;

internal sealed class GetAllCustomerQueryHandler(
    ICustomerRepository customerRepository) : IRequestHandler<GetAllCustomerQuery, Result<List<Customer>>>
{
    public async Task<Result<List<Customer>>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
    {
        List<Customer> customers = await customerRepository.GetAll().OrderBy(p => p.Name).ToListAsync(cancellationToken);

        return customers;
    }
    
}