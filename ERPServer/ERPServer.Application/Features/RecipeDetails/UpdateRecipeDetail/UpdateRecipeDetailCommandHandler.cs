using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Respositories;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.RecipeDetails.UpdateRecipeDetail;

internal sealed class UpdateRecipeDetailCommandHandler(
    IRecipeDetailRepository recipeDetailRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateRecipeDetailCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateRecipeDetailCommand request, CancellationToken cancellationToken)
    {
        RecipeDetail recipeDetail =
            await recipeDetailRepository.GetByExpressionWithTrackingAsync(
                p => p.Id == request.Id,
                cancellationToken);

        if (recipeDetail is null)
        {
            return Result<string>.Failure("Seçilen Ürün Reçetede Bulunamadı");
        }


        RecipeDetail? oldRecipeDetail =
                    await recipeDetailRepository.Where(
                     p => p.Id != request.Id &&
                     p.ProductId == request.ProductId &&
                     p.RecipeId  == recipeDetail.RecipeId)
                     .FirstOrDefaultAsync(cancellationToken);
        
        // İstekde gelen ürün bu reçetede başka bir satırda var mı? varsa bu kaydı temizlememiz gerekiyor...
        // yani örneğin bir ürün reçetesinde çıta ve kumaş var diyelim. ben gidip çıtayı kumaş ile değiştirmek istersem mevcuttaki çıta satırını bulup
        // o satırı silip daha sonra mevcuttaki kumaş adetini istekde gelen adet ile set ederek bilgi tutarlılığını saplamlaştırmış oluyoruz

        if (oldRecipeDetail is not null)
        {
            recipeDetailRepository.Delete(recipeDetail);

            oldRecipeDetail.Quantity += request.Quantity;
            
            recipeDetailRepository.Update(oldRecipeDetail);

        }
        else
        {
            mapper.Map(request, recipeDetail);

        }
        
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Reçetedeki ilgili ürün başarıyla güncellendi";
    }
}