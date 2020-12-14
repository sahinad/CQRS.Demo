namespace CQRS.Demo.Domain.Queries
{
    public interface IQueryFactory
    {
        TQueryObject CreateQuery<TQueryObject>() where TQueryObject : IQueryObject;
    }
}
