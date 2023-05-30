namespace Fohjin.DDD.Commands
{
    public abstract class CommandBase : ICommand
    {
        public Guid Id { get; set; }

        public CommandBase()
        {
        }
        public CommandBase(Guid id)
        {
            Id = id;
        }
    }
}