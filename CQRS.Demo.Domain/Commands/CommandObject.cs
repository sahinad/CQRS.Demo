using CQRS.Demo.Domain.Models;
using System.Threading.Tasks;

namespace CQRS.Demo.Domain.Commands
{
    public class CommandObject<TEntity> : ICommandObject<TEntity> where TEntity : BaseModel
    {
        protected DomainContext DbContext { get; }

        public CommandObject(IDomainContextFactory domainContextFactory)
        {
            DbContext = domainContextFactory.CreateDbContext();
        }

        public async Task AddOrUpdateAsync(TEntity entity)
        {
            if (entity.Id == 0)
            {
                DbContext.Set<TEntity>().Add(entity);
            }
            else
            {
                DbContext.Set<TEntity>().Update(entity);
            }

            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
            await DbContext.SaveChangesAsync();
        }
    }
}
