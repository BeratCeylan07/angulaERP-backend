using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Respositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Depots.UpdateDepot;

internal sealed class UpdateDepotCommandHandler(
    IDepotRepository depotRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateDepotCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateDepotCommand request, CancellationToken cancellationToken)
    {
        Depot depot = await depotRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.Id, cancellationToken);
        
        if (depot is null)
        {
            return Result<string>.Failure("Depo Bulunamadı");
        }

        mapper.Map(request, depot);
        
        // Yukarda kullandığımız GetByExpressionWithTrackingAsync metodu sayesinde Repositoryden update metodunu tetiklememize gerek kalmıyor map ile aktarım yapmamız yeterli oluyor

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Depo Bilgileri Güncellendi";
    }
}