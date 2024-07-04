using ERPServer.Domain.Entities;
using ERPServer.Domain.Respositories;
using ERPServer.Infrastructure.Context;
using GenericRepository;

namespace ERPServer.Infrastructure.Repositories;

public sealed class RecipeRepository : Repository<Recipe, ApplicationDbContext>, IRecipeRepository
{
    public RecipeRepository(ApplicationDbContext context) : base(context)
    {
    }
}