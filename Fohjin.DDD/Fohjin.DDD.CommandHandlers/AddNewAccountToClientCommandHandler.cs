using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain.Entities;

namespace Fohjin.DDD.CommandHandlers
{
    public class AddNewAccountToClientCommandHandler : ICommandHandler<AddNewAccountToClientCommand>
    {
        private readonly IDomainRepository _repository;

        public AddNewAccountToClientCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public void Execute(AddNewAccountToClientCommand command)
        {
            var client = _repository.GetById<Client>(command.Id);
            var activeAccount = client.CreateNewAccount(command.AccountName);

            _repository.Save(activeAccount);
            _repository.Save(client);
        }
    }
}