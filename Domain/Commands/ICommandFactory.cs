namespace Domain.Commands
{
    using Contexts;

    public interface ICommandFactory
    {
        ICommand<TCommandContext> Create<TCommandContext>() where TCommandContext : ICommandContext;
    }
}