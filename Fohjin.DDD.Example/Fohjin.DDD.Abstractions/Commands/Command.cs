namespace Fohjin.DDD.Commands
{
    public abstract record Command : ICommand
    {
        public Guid Id { get; init; }

        public Command(Guid id)
        {
            Id = id;
        }
    }
}