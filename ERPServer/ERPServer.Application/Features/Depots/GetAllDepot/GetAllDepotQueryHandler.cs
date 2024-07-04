using ERPServer.Domain.Entities;
using ERPServer.Domain.Respositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Depots.GetAllDepot;

public sealed class GetAllDepotQueryHandler(
    IDepotRepository depotRepository) : IRequestHandler<GetAllDepotQuery, Result<List<Depot>>>
{
    public async Task<Result<List<Depot>>> Handle(GetAllDepotQuery request, CancellationToken cancellationToken)
    {
        List<Depot> depots = await depotRepository.GetAll().OrderBy(o => o.Name).ToListAsync(cancellationToken);
        return depots;
    }
}