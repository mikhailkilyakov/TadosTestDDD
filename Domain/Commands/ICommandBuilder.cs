namespace Domain.Commands
{
    using Contexts;

    public interface ICommandBuilder
    {
        void Execute<TCommandContext>(TCommandContext commandContext) where TCommandContext : ICommandContext;
    }
}