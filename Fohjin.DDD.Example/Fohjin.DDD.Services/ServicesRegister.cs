using StructureMap.Configuration.DSL;

namespace Fohjin.DDD.Services
{
    public class ServicesRegister : Registry
    {
        public ServicesRegister()
        {
            For<IReceiveMoneyTransfers>().Use<MoneyReceiveService>();
            For<ISendMoneyTransfer>().Use<MoneyTransferService>();
        }
    }
}