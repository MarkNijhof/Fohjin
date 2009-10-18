using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Entities;
using Fohjin.EventStorage;

namespace Fohjin.DDD.CommandHandlers
{
    public class AddNewAccountToClientCommandHandler : ICommandHandler<AddNewAccountToClientCommand>
    {
        private readonly IRepository _repository;

        public AddNewAccountToClientCommandHandler(IRepository activeAccountRepository)
        {
            _repository = activeAccountRepository;
        }

        public void Execute(AddNewAccountToClientCommand command)
        {
            var client = _repository.GetById<Client>(command.Id);
            ActiveAccount activeAccount = client.CreateNewAccount(command.AccountName);

            _repository.Save(activeAccount);
            _repository.Save(client);
        }
    }
}