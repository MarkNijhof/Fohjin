using Fohjin.DDD.Commands;
using StructureMap.Configuration.DSL;

namespace Fohjin.DDD.CommandHandlers
{
    public class CommandHandlerRegister : Registry
    {
        public CommandHandlerRegister()
        {
            ForRequestedType<ICommandHandler<ClientHasMovedCommand>>().TheDefaultIsConcreteType<ClientHasMovedCommandHandler>();
            ForRequestedType<ICommandHandler<ClientPhoneNumberIsChangedCommand>>().TheDefaultIsConcreteType<ClientPhoneNumberIsChangedCommandHandler>();

            ForRequestedType<ICommandHandler<AddNewAccountToClientCommand>>().TheDefaultIsConcreteType<AddNewAccountToClientCommandHandler>();
            ForRequestedType<ICommandHandler<CashDepositeCommand>>().TheDefaultIsConcreteType<MakeADepositeOnAnAccountCommandHandler>();
            ForRequestedType<ICommandHandler<CashWithdrawlCommand>>().TheDefaultIsConcreteType<MakeAWithdrawlFromAnAccountCommandHandler>();
            ForRequestedType<ICommandHandler<CloseAccountCommand>>().TheDefaultIsConcreteType<CloseAnAccountCommandHandler>();
        }
    }
}