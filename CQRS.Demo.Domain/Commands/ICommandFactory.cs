namespace CQRS.Demo.Domain.Commands
{
    public interface ICommandFactory
    {
        TCommandObject CreateCommand<TCommandObject>() where TCommandObject : ICommandObject;
    }
}
