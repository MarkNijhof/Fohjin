
using StructureMap.Configuration.DSL;

namespace Fohjin.DDD.Services
{
    public class ServicesRegister : Registry
    {
        public ServicesRegister()
        {
            ForRequestedType<IAcceptMoneyTransfer>().TheDefault.Is.OfConcreteType<AcceptMoneyTransferService>();
            ForRequestedType<ISendMoneyTransfer>().TheDefault.Is.OfConcreteType<SendMoneyTransferService>();
        }
    }
}