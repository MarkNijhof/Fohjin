using System;
using System.Linq;
using Fohjin.DDD.Configuration;
using Fohjin.DDD.Reporting.Dto;
using Fohjin.DDD.Reporting.Infrastructure;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Test.Fohjin.DDD.Reporting.Infrastructure
{
    [TestFixture]
    public class RepositoryTest
    {
        private Repository _repository;
        private const string dataBaseFile = "reportingDataBase.db3";

        [SetUp]
        public void SetUp()
        {
            new ReportingDatabaseBootStrapper().ReCreateDatabaseSchema();
            
            var sqliteConnectionString = string.Format("Data Source={0}", dataBaseFile);

            _repository = new Repository(sqliteConnectionString, new SqlSelectBuilder(), new SqlInsertBuilder(), new SqlUpdateBuilder());
        }

        [Test]
        public void Will_be_able_to_save_and_retrieve_a_client_dto()
        {
            var clientDto = new Client(Guid.NewGuid(), "Mark Nijhof");
            _repository.Save(clientDto);
            var sut = _repository.GetByExample<Client>(new {Name = "Mark Nijhof"}).FirstOrDefault();

            Assert.That(sut.Id, Is.EqualTo(clientDto.Id));
            Assert.That(sut.Name, Is.EqualTo(clientDto.Name));
        }

        [Test]
        public void Will_be_able_to_save_and_retrieve_a_client_details_dto()
        {
            var clientDetailsDto = new ClientDetails(Guid.NewGuid(), "Mark Nijhof", "Street", "123", "5006", "Bergen", "123456789");
            _repository.Save(clientDetailsDto);
            var sut = _repository.GetByExample<ClientDetails>(new {ClientName = "Mark Nijhof"}).FirstOrDefault();

            Assert.That(sut.Id, Is.EqualTo(clientDetailsDto.Id));
            Assert.That(sut.ClientName, Is.EqualTo(clientDetailsDto.ClientName));
            Assert.That(sut.Street, Is.EqualTo(clientDetailsDto.Street));
            Assert.That(sut.StreetNumber, Is.EqualTo(clientDetailsDto.StreetNumber));
            Assert.That(sut.PostalCode, Is.EqualTo(clientDetailsDto.PostalCode));
            Assert.That(sut.City, Is.EqualTo(clientDetailsDto.City));
            Assert.That(sut.PhoneNumber, Is.EqualTo(clientDetailsDto.PhoneNumber));
        }

        [Test]
        public void Will_be_able_to_save_and_retrieve_an_account_dto()
        {
            var accountDto = new Account(Guid.NewGuid(), Guid.NewGuid(), "Account Name", "1234567890", true);
            _repository.Save(accountDto);
            var sut = _repository.GetByExample<Account>(new { Name = "Account Name" }).FirstOrDefault();

            Assert.That(sut.Id, Is.EqualTo(accountDto.Id));
            Assert.That(sut.ClientDetailsId, Is.EqualTo(accountDto.ClientDetailsId));
            Assert.That(sut.Name, Is.EqualTo(accountDto.Name));
            Assert.That(sut.AccountNumber, Is.EqualTo(accountDto.AccountNumber));
        }

        [Test]
        public void Will_be_able_to_save_and_retrieve_an_account_details_dto()
        {
            var accountDetailsDto = new AccountDetails(Guid.NewGuid(), Guid.NewGuid(), "Account Name", 10.5M, "1234567890", true);
            _repository.Save(accountDetailsDto);
            var sut = _repository.GetByExample<AccountDetails>(new { AccountName = "Account Name" }).FirstOrDefault();

            Assert.That(sut.Id, Is.EqualTo(accountDetailsDto.Id));
            Assert.That(sut.ClientId, Is.EqualTo(accountDetailsDto.ClientId));
            Assert.That(sut.AccountName, Is.EqualTo(accountDetailsDto.AccountName));
            Assert.That(sut.Balance, Is.EqualTo(accountDetailsDto.Balance));
            Assert.That(sut.AccountNumber, Is.EqualTo(accountDetailsDto.AccountNumber));
        }

        [Test]
        public void Will_be_able_to_save_and_retrieve_a_ledger_dto()
        {
            var ledgerDto = new Ledger(Guid.NewGuid(), Guid.NewGuid(), "Action", 12.3M);
            _repository.Save(ledgerDto);
            var sut = _repository.GetByExample<Ledger>(new { Action = "Action", Amount = 12.3M }).FirstOrDefault();

            Assert.That(sut.Id, Is.EqualTo(ledgerDto.Id));
            Assert.That(sut.AccountDetailsId, Is.EqualTo(ledgerDto.AccountDetailsId));
            Assert.That(sut.Amount, Is.EqualTo(ledgerDto.Amount));
            Assert.That(sut.Action, Is.EqualTo(ledgerDto.Action));
        }

        [Test]
        public void When_calling_GetByExample_it_will_return_a_list_with_dtos_matching_the_example()
        {
            _repository.Save(new Client(Guid.NewGuid(), "Mark Nijhof"));
            _repository.Save(new Client(Guid.NewGuid(), "Mark Nijhof"));
            var sut = _repository.GetByExample<Client>(new { Name = "Mark Nijhof" });

            Assert.That(sut.Count(), Is.EqualTo(2));
        }

        [Test]
        public void When_calling_GetByExample_it_will_return_a_list_with_dtos_matching_the_example_inclusing_child_objects()
        {
            var AccountId = Guid.NewGuid();
            _repository.Save(new AccountDetails(AccountId, Guid.NewGuid(), "Account Name", 10.5M, "1234567890", true));

            _repository.Save(new Ledger(Guid.NewGuid(), AccountId, "Action 1", 12.3M));
            _repository.Save(new Ledger(Guid.NewGuid(), AccountId, "Action 2", 24.6M));
            _repository.Save(new Ledger(Guid.NewGuid(), Guid.NewGuid(), "Action 3", 96.3M));

            var sut = _repository.GetByExample<AccountDetails>(new { AccountName = "Account Name" }).FirstOrDefault();

            Assert.That(sut.Ledgers.Count(), Is.EqualTo(2));
            Assert.That(sut.Ledgers.First().Action, Is.EqualTo("Action 1"));
            Assert.That(sut.Ledgers.First().Amount, Is.EqualTo(12.3M));
            Assert.That(sut.Ledgers.Last().Action, Is.EqualTo("Action 2"));
            Assert.That(sut.Ledgers.Last().Amount, Is.EqualTo(24.6M));
        }

        [Test]
        public void When_be_able_to_update_an_already_saved_dto()
        {
            Guid guid = Guid.NewGuid();
            _repository.Save(new Client(guid, "Mark Nijhof"));

            _repository.Update<Client>(new { Name = "Mark Albert Nijhof" }, new { Id = guid });

            var sut = _repository.GetByExample<Client>(new { Id = guid });

            Assert.That(sut.Count(), Is.EqualTo(1));
            Assert.That(sut.First().Name, Is.EqualTo("Mark Albert Nijhof"));
        }
    }
}