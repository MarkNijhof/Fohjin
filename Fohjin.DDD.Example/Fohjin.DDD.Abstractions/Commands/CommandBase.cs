namespace Fohjin.DDD.Commands
{
    public abstract record CommandBase : ICommand
    {
        public Guid Id { get; init; }

        public CommandBase()
        {
        }
        public CommandBase(Guid id)
        {
            Id = id;
        }
    }
}