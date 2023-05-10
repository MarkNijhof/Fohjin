namespace Fohjin.DDD.Commands
{
    public record CloseAccountCommand : Command
    {
        public CloseAccountCommand(Guid id) : base(id) { }
    }
}