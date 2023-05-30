namespace Fohjin.DDD.Commands
{
    public abstract class Command : ICommand
    {
        public Guid Id { get; set; }

        public Command(Guid id)
        {
            Id = id;
        }
    }
}