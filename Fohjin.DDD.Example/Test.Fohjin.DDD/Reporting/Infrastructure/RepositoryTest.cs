using System;
using System.Linq;
using Fohjin.DDD.Configuration;
using Fohjin.DDD.Reporting.Dto;
using Fohjin.DDD.Reporting.Infrastructure;
using NUnit.Framework;


namespace Test.Fohjin.DDD.Reporting.Infrastructure
{
    [TestFixture]
    public class RepositoryTest
    {
        private SQLiteReportingRepository _repository;
        private const string dataBaseFile = "reportingDataBase.db3";

        [SetUp]
        public void SetUp()
        {
            new ReportingDatabaseBootStrapper().ReCreateDatabaseSchema();
            
            var sqliteConnectionString = string.Format("Data Source={0}", dataBaseFile);

            _repository = new SQLiteReportingRepository(sqliteConnectionString, new SqlSelectBuilder(), new SqlInsertBuilder(), new SqlUpdateBuilder(), new SqlDeleteBuilder());
        }

        [Test]
        public void Will_be_able_to_save_and_retrieve_a_client_dto()
        {
            var clientDto = new ClientReport(Guid.NewGuid(), "Mark Nijhof");
            _repository.Save(clientDto);
            var sut = _repository.GetByExample<ClientReport>(new {Name = "Mark Nijhof"}).FirstOrDefault();

            Assert.That(sut.Id, Is.EqualTo(clientDto.Id));
            Assert.That(sut.Name, Is.EqualTo(clientDto.Name));
        }

        [Test]
        public void Will_be_able_to_save_and_retrieve_a_client_details_dto()
        {
            var clientDetailsDto = new ClientDetailsReport(Guid.NewGuid(), "Mark Nijhof", "Street", "123", "5006", "Bergen", "123456789");
            _repository.Save(clientDetailsDto);
            var sut = _repository.GetByExample<ClientDetailsReport>(new {ClientName = "Mark Nijhof"}).FirstOrDefault();

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
            var accountDto = new AccountReport(Guid.NewGuid(), Guid.NewGuid(), "Account Name", "1234567890");
            _repository.Save(accountDto);
            var sut = _repository.GetByExample<AccountReport>(new { AccountName = "Account Name" }).FirstOrDefault();

            Assert.That(sut.Id, Is.EqualTo(accountDto.Id));
            Assert.That(sut.ClientDetailsReportId, Is.EqualTo(accountDto.ClientDetailsReportId));
            Assert.That(sut.AccountName, Is.EqualTo(accountDto.AccountName));
            Assert.That(sut.AccountNumber, Is.EqualTo(accountDto.AccountNumber));
        }

        [Test]
        public void Will_be_able_to_save_and_retrieve_an_account_details_dto()
        {
            var accountDetailsDto = new AccountDetailsReport(Guid.NewGuid(), Guid.NewGuid(), "Account Name", 10.5M, "1234567890");
            _repository.Save(accountDetailsDto);
            var sut = _repository.GetByExample<AccountDetailsReport>(new { AccountName = "Account Name" }).FirstOrDefault();

            Assert.That(sut.Id, Is.EqualTo(accountDetailsDto.Id));
            Assert.That(sut.ClientReportId, Is.EqualTo(accountDetailsDto.ClientReportId));
            Assert.That(sut.AccountName, Is.EqualTo(accountDetailsDto.AccountName));
            Assert.That(sut.Balance, Is.EqualTo(accountDetailsDto.Balance));
            Assert.That(sut.AccountNumber, Is.EqualTo(accountDetailsDto.AccountNumber));
        }

        [Test]
        public void Will_be_able_to_save_and_retrieve_a_ledger_dto()
        {
            var ledgerDto = new LedgerReport(Guid.NewGuid(), Guid.NewGuid(), "Action", 12.3M);
            _repository.Save(ledgerDto);
            var sut = _repository.GetByExample<LedgerReport>(new { Action = "Action", Amount = 12.3M }).FirstOrDefault();

            Assert.That(sut.Id, Is.EqualTo(ledgerDto.Id));
            Assert.That(sut.AccountDetailsReportId, Is.EqualTo(ledgerDto.AccountDetailsReportId));
            Assert.That(sut.Amount, Is.EqualTo(ledgerDto.Amount));
            Assert.That(sut.Action, Is.EqualTo(ledgerDto.Action));
        }

        [Test]
        public void When_calling_GetByExample_it_will_return_a_list_with_dtos_matching_the_example()
        {
            _repository.Save(new ClientReport(Guid.NewGuid(), "Mark Nijhof"));
            _repository.Save(new ClientReport(Guid.NewGuid(), "Mark Nijhof"));
            var sut = _repository.GetByExample<ClientReport>(new { Name = "Mark Nijhof" });

            Assert.That(sut.Count(), Is.EqualTo(2));
        }

        [Test]
        public void When_calling_GetByExample_it_will_return_a_list_with_dtos_matching_the_example_inclusing_child_objects()
        {
            var AccountId = Guid.NewGuid();
            _repository.Save(new AccountDetailsReport(AccountId, Guid.NewGuid(), "Account Name", 10.5M, "1234567890"));

            _repository.Save(new LedgerReport(Guid.NewGuid(), AccountId, "Action 1", 12.3M));
            _repository.Save(new LedgerReport(Guid.NewGuid(), AccountId, "Action 2", 24.6M));
            _repository.Save(new LedgerReport(Guid.NewGuid(), Guid.NewGuid(), "Action 3", 96.3M));

            var sut = _repository.GetByExample<AccountDetailsReport>(new { AccountName = "Account Name" }).FirstOrDefault();

            Assert.That(sut.Ledgers.Count(), Is.EqualTo(2));
            Assert.That(sut.Ledgers.First().Action, Is.EqualTo("Action 1"));
            Assert.That(sut.Ledgers.First().Amount, Is.EqualTo(12.3M));
            Assert.That(sut.Ledgers.Last().Action, Is.EqualTo("Action 2"));
            Assert.That(sut.Ledgers.Last().Amount, Is.EqualTo(24.6M));
        }

        [Test]
        public void Will_be_able_to_update_an_already_saved_dto()
        {
            Guid guid = Guid.NewGuid();
            _repository.Save(new ClientReport(guid, "Mark Nijhof"));

            _repository.Update<ClientReport>(new { Name = "Mark Albert Nijhof" }, new { Id = guid });

            var sut = _repository.GetByExample<ClientReport>(new { Id = guid });

            Assert.That(sut.Count(), Is.EqualTo(1));
            Assert.That(sut.First().Name, Is.EqualTo("Mark Albert Nijhof"));
        }

        [Test]
        public void Will_be_able_to_delete_an_already_saved_dto()
        {
            Guid guid = Guid.NewGuid();
            _repository.Save(new ClientReport(guid, "Mark Nijhof"));

            _repository.Delete<ClientReport>(new { Id = guid });

            var sut = _repository.GetByExample<ClientReport>(new { Id = guid });

            Assert.That(sut.Count(), Is.EqualTo(0));
        }
    }
}