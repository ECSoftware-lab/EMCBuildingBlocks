using Microsoft.EntityFrameworkCore;

namespace EMC.BuildingBlocks.Repository
{
    public interface ITransactionManager
    {
        Task<DbContext> BeginTransactionAsync();
        Task CommitTransactionAsync(DbContext context);
        Task RollbackTransactionAsync(DbContext context);
        Task<int> SaveChangesAsync(DbContext context);
    }
}