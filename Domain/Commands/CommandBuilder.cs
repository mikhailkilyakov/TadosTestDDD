namespace Domain.Commands
{
    using Contexts;

    public class CommandBuilder : ICommandBuilder
    {
        private readonly ICommandFactory _commandFactory;

        public CommandBuilder(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
        }
        
        public void Execute<TCommandContext>(TCommandContext commandContext) where TCommandContext : ICommandContext
        {
            _commandFactory.Create<TCommandContext>().Execute(commandContext);
        }
    }
}