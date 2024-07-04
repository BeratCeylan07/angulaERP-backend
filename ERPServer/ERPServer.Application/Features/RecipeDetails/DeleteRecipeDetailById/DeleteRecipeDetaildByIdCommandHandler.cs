using ERPServer.Domain.Entities;
using ERPServer.Domain.Respositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.RecipeDetails.DeleteRecipeDetailById;

internal sealed class DeleteRecipeDetaildByIdCommandHandler(
    IRecipeDetailRepository recipeDetailRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteRecipeDetailByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteRecipeDetailByIdCommand request, CancellationToken cancellationToken)
    {
        RecipeDetail recipeDetail =
            await recipeDetailRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);

        if (recipeDetail is null)
        {
            return Result<string>.Failure("Reçete'de Bu Ürün Bulunamadı");
        }

        recipeDetailRepository.Delete(recipeDetail);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Fiş Ürünü Silindi Silindi";
    }
}