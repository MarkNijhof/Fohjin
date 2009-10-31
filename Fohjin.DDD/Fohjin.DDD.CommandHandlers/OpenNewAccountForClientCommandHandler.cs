using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain.Client;

namespace Fohjin.DDD.CommandHandlers
{
    public class OpenNewAccountForClientCommandHandler : ICommandHandler<OpenNewAccountForClientCommand>
    {
        private readonly IDomainRepository _repository;

        public OpenNewAccountForClientCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public void Execute(OpenNewAccountForClientCommand compensatingCommand)
        {
            var client = _repository.GetById<Client>(compensatingCommand.Id);
            var activeAccount = client.CreateNewAccount(compensatingCommand.AccountName);

            _repository.Save(activeAccount);
            _repository.Save(client);
        }
    }
}