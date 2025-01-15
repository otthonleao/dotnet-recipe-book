using MyRecipeBook.Domain.Repositories;

namespace MyRecipeBook.Infrastructure.DatabaseAccess;

public class UnitOfWork : IUnitWork
{
    private readonly MyRecipeBookDbContext _dbContext;
    
    public UnitOfWork(MyRecipeBookDbContext context) => _dbContext = context;
    
    public async Task Commit() => await _dbContext.SaveChangesAsync();
}