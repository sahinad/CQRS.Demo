using CQRS.Demo.Domain.Models;
using System.Threading.Tasks;

namespace CQRS.Demo.Domain.Commands
{
    public interface ICommandObject
    {

    }

    public interface ICommandObject<in TEntity> : ICommandObject where TEntity : BaseModel
    {
        Task AddOrUpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
    }
}
