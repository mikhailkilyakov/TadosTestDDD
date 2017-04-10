namespace Domain.Commands
{
    using Contexts;
    public interface ICommand<in TCommandContext> where TCommandContext : ICommandContext
    {
        void Execute(TCommandContext commandContext);
    }
}