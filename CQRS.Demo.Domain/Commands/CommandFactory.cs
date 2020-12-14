using Microsoft.Extensions.DependencyInjection;
using System;

namespace CQRS.Demo.Domain.Commands
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TCommandObject CreateCommand<TCommandObject>() where TCommandObject : ICommandObject
        {
            return _serviceProvider.GetRequiredService<TCommandObject>();
        }
    }
}
