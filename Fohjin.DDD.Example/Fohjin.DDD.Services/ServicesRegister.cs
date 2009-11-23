using StructureMap.Configuration.DSL;

namespace Fohjin.DDD.Services
{
    public class ServicesRegister : Registry
    {
        public ServicesRegister()
        {
            ForRequestedType<IReceiveMoneyTransfers>().TheDefault.Is.OfConcreteType<MoneyReceiveService>();
            ForRequestedType<ISendMoneyTransfer>().TheDefault.Is.OfConcreteType<MoneyTransferService>();
        }
    }
}