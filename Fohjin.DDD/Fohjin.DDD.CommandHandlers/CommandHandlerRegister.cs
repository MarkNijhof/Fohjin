using System;
using System.Linq;
using Fohjin.DDD.Commands;
using StructureMap.Configuration.DSL;

namespace Fohjin.DDD.CommandHandlers
{
    public class CommandHandlerRegister : Registry
    {
        public CommandHandlerRegister()
        {
            var commandTypes = typeof(Command).Assembly.GetExportedTypes().Where(x => x.BaseType == typeof(Command)).ToList();
            var handlerTypes = typeof(ICommandHandler<>).Assembly.GetExportedTypes().Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(ICommandHandler<>))).ToList();

            foreach (var type in commandTypes)
            {
                var handlerType = handlerTypes.SingleOrDefault(x => x.Name == string.Format("{0}Handler", type.Name));

                if (handlerType == null)
                    throw new Exception(string.Format("There is no handler registered for command {0}, expected handler is {0}Handler", type.Name));

                ForRequestedType(typeof (ICommandHandler<>).MakeGenericType(type))
                    .TheDefaultIsConcreteType(handlerType);
            }


            //ForRequestedType<ICommandHandler<ClientHasMovedCommand>>().TheDefaultIsConcreteType<ClientHasMovedCommandHandler>();
            //ForRequestedType<ICommandHandler<ClientPhoneNumberIsChangedCommand>>().TheDefaultIsConcreteType<ClientPhoneNumberIsChangedCommandHandler>();

            //ForRequestedType<ICommandHandler<AddNewAccountToClientCommand>>().TheDefaultIsConcreteType<AddNewAccountToClientCommandHandler>();
            //ForRequestedType<ICommandHandler<CashDepositeCommand>>().TheDefaultIsConcreteType<CashDepositeCommandHandler>();
            //ForRequestedType<ICommandHandler<CashWithdrawlCommand>>().TheDefaultIsConcreteType<CashWithdrawlCommandHandler>();
            //ForRequestedType<ICommandHandler<CloseAccountCommand>>().TheDefaultIsConcreteType<CloseAnAccountCommandHandler>();
        }
    }
}