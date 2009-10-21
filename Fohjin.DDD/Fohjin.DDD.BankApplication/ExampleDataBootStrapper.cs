using System;
using Fohjin.DDD.Reporting.Dto;
using Fohjin.DDD.Reporting.Infrastructure;
using StructureMap;

namespace Fohjin.DDD.BankApplication
{
    public class ExampleDataBootStrapper
    {
        public void BootStrapTheExampleData()
        {
            var repository = ObjectFactory.GetInstance<IReportingRepository>();

            var clientDto1 = new Client(Guid.NewGuid(), "Mark Nijhof");
            var clientDto2 = new Client(Guid.NewGuid(), "Mona Nijhof");

            repository.Save(clientDto1);
            repository.Save(clientDto2);

            var clientDetailsDto1 = new ClientDetails(clientDto1.Id, "Mark Nijhof", "Welhavens gate", "49b", "5006", "Bergen", "95009937");
            var clientDetailsDto2 = new ClientDetails(clientDto2.Id, "Mona Nijhof", "Welhavens gate", "49b", "5006", "Bergen", "12345678");

            repository.Save(clientDetailsDto1);
            repository.Save(clientDetailsDto2);

            var accountDto1 = new Account(Guid.NewGuid(), clientDto1.Id, "Private Account Mark Nijhof", "1122334455", true);
            var accountDto2 = new Account(Guid.NewGuid(), clientDto1.Id, "Business Account Mark Nijhof", "5544332211", true);
            var accountDto3 = new Account(Guid.NewGuid(), clientDto2.Id, "Private Account Mona Nijhof", "9977553311", true);

            repository.Save(accountDto1);
            repository.Save(accountDto2);
            repository.Save(accountDto3);

            var accountDetailsDto1 = new AccountDetails(accountDto1.Id, clientDto1.Id, "Private Account Mark Nijhof", 10.5M, "1122334455", true);
            var accountDetailsDto2 = new AccountDetails(accountDto2.Id, clientDto1.Id, "Business Account Mark Nijhof", 10.5M, "5544332211", true);
            var accountDetailsDto3 = new AccountDetails(accountDto3.Id, clientDto2.Id, "Private Account Mona Nijhof", 10.5M, "9977553311", true);

            repository.Save(accountDetailsDto1);
            repository.Save(accountDetailsDto2);
            repository.Save(accountDetailsDto3);

            var ledgerDto1 = new Ledger(Guid.NewGuid(), accountDto1.Id, "Deposite", 12.3M);
            var ledgerDto2 = new Ledger(Guid.NewGuid(), accountDto1.Id, "Deposite", 12.3M);
            var ledgerDto3 = new Ledger(Guid.NewGuid(), accountDto1.Id, "Withdrawl", 12.3M);
            var ledgerDto4 = new Ledger(Guid.NewGuid(), accountDto1.Id, "Deposite", 12.3M);
            var ledgerDto5 = new Ledger(Guid.NewGuid(), accountDto1.Id, "Withdrawl", 12.3M);
            var ledgerDto6 = new Ledger(Guid.NewGuid(), accountDto2.Id, "Deposite", 12.3M);
            var ledgerDto7 = new Ledger(Guid.NewGuid(), accountDto2.Id, "Withdrawl", 12.3M);
            var ledgerDto8 = new Ledger(Guid.NewGuid(), accountDto2.Id, "Withdrawl", 12.3M);
            var ledgerDto9 = new Ledger(Guid.NewGuid(), accountDto2.Id, "Deposite", 12.3M);
            var ledgerDto10 = new Ledger(Guid.NewGuid(), accountDto3.Id, "Deposite", 12.3M);
            var ledgerDto11 = new Ledger(Guid.NewGuid(), accountDto3.Id, "Withdrawl", 12.3M);
            var ledgerDto12 = new Ledger(Guid.NewGuid(), accountDto3.Id, "Withdrawl", 12.3M);

            repository.Save(ledgerDto1);
            repository.Save(ledgerDto2);
            repository.Save(ledgerDto3);
            repository.Save(ledgerDto4);
            repository.Save(ledgerDto5);
            repository.Save(ledgerDto6);
            repository.Save(ledgerDto7);
            repository.Save(ledgerDto8);
            repository.Save(ledgerDto9);
            repository.Save(ledgerDto10);
            repository.Save(ledgerDto11);
            repository.Save(ledgerDto12);
        }

        public static void BootStrap()
        {
            new ExampleDataBootStrapper().BootStrapTheExampleData();
        }
    }
}