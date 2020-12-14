using Microsoft.Extensions.DependencyInjection;
using System;

namespace CQRS.Demo.Domain.Queries
{
    public class QueryFactory : IQueryFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TQueryObject CreateQuery<TQueryObject>() where TQueryObject : IQueryObject
        {
            return _serviceProvider.GetRequiredService<TQueryObject>();
        }
    }
}
