using System;
using System.Linq;
using Fohjin.DDD.Configuration;
using Fohjin.DDD.Reporting.Dtos;
using Fohjin.DDD.Reporting.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework.SyntaxHelpers;

namespace Test.Fohjin.DDD.Reporting.Infrastructure
{
    [TestClass]
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

        [TestMethod]
        public void Will_be_able_to_save_and_retrieve_a_client_dto()
        {
            var clientDto = new ClientReport(Guid.NewGuid(), "Mark Nijhof");
            _repository.Save(clientDto);
            var sut = _repository.GetByExample<ClientReport>(new { Name = "Mark Nijhof" }).FirstOrDefault();

            Assert.AreEqual(sut.Id, Is.EqualTo(clientDto.Id));
            Assert.AreEqual(sut.Name, Is.EqualTo(clientDto.Name));
        }

        [TestMethod]
        public void Will_be_able_to_save_and_retrieve_a_client_details_dto()
        {
            var clientDetailsDto = new ClientDetailsReport(Guid.NewGuid(), "Mark Nijhof", "Street", "123", "5006", "Bergen", "123456789");
            _repository.Save(clientDetailsDto);
            var sut = _repository.GetByExample<ClientDetailsReport>(new { ClientName = "Mark Nijhof" }).FirstOrDefault();

            Assert.AreEqual(sut.Id, Is.EqualTo(clientDetailsDto.Id));
            Assert.AreEqual(sut.ClientName, Is.EqualTo(clientDetailsDto.ClientName));
            Assert.AreEqual(sut.Street, Is.EqualTo(clientDetailsDto.Street));
            Assert.AreEqual(sut.StreetNumber, Is.EqualTo(clientDetailsDto.StreetNumber));
            Assert.AreEqual(sut.PostalCode, Is.EqualTo(clientDetailsDto.PostalCode));
            Assert.AreEqual(sut.City, Is.EqualTo(clientDetailsDto.City));
            Assert.AreEqual(sut.PhoneNumber, Is.EqualTo(clientDetailsDto.PhoneNumber));
        }

        [TestMethod]
        public void Will_be_able_to_save_and_retrieve_an_account_dto()
        {
            var accountDto = new AccountReport(Guid.NewGuid(), Guid.NewGuid(), "Account Name", "1234567890");
            _repository.Save(accountDto);
            var sut = _repository.GetByExample<AccountReport>(new { AccountName = "Account Name" }).FirstOrDefault();

            Assert.AreEqual(accountDto.Id, sut.Id);
            Assert.AreEqual(accountDto.ClientDetailsReportId, sut.ClientDetailsReportId);
            Assert.AreEqual(accountDto.AccountName, sut.AccountName);
            Assert.AreEqual(accountDto.AccountNumber, sut.AccountNumber);
        }

        [TestMethod]
        public void Will_be_able_to_save_and_retrieve_an_account_details_dto()
        {
            var accountDetailsDto = new AccountDetailsReport(Guid.NewGuid(), Guid.NewGuid(), "Account Name", 10.5M, "1234567890");
            _repository.Save(accountDetailsDto);
            var sut = _repository.GetByExample<AccountDetailsReport>(new { AccountName = "Account Name" }).FirstOrDefault();

            Assert.AreEqual(accountDetailsDto.Id, sut.Id);
            Assert.AreEqual(accountDetailsDto.ClientReportId, sut.ClientReportId);
            Assert.AreEqual(accountDetailsDto.AccountName, sut.AccountName);
            Assert.AreEqual(accountDetailsDto.Balance, sut.Balance);
            Assert.AreEqual(accountDetailsDto.AccountNumber, sut.AccountNumber);
        }

        [TestMethod]
        public void Will_be_able_to_save_and_retrieve_a_ledger_dto()
        {
            var ledgerDto = new LedgerReport(Guid.NewGuid(), Guid.NewGuid(), "Action", 12.3M);
            _repository.Save(ledgerDto);
            var sut = _repository.GetByExample<LedgerReport>(new { Action = "Action", Amount = 12.3M }).FirstOrDefault();

            Assert.AreEqual(sut.Id, Is.EqualTo(ledgerDto.Id));
            Assert.AreEqual(sut.AccountDetailsReportId, Is.EqualTo(ledgerDto.AccountDetailsReportId));
            Assert.AreEqual(sut.Amount, Is.EqualTo(ledgerDto.Amount));
            Assert.AreEqual(sut.Action, Is.EqualTo(ledgerDto.Action));
        }

        [TestMethod]
        public void When_calling_GetByExample_it_will_return_a_list_with_dtos_matching_the_example()
        {
            _repository.Save(new ClientReport(Guid.NewGuid(), "Mark Nijhof"));
            _repository.Save(new ClientReport(Guid.NewGuid(), "Mark Nijhof"));
            var sut = _repository.GetByExample<ClientReport>(new { Name = "Mark Nijhof" });

            Assert.AreEqual(2, sut.Count());
        }

        [TestMethod]
        public void When_calling_GetByExample_it_will_return_a_list_with_dtos_matching_the_example_inclusing_child_objects()
        {
            var AccountId = Guid.NewGuid();
            _repository.Save(new AccountDetailsReport(AccountId, Guid.NewGuid(), "Account Name", 10.5M, "1234567890"));

            _repository.Save(new LedgerReport(Guid.NewGuid(), AccountId, "Action 1", 12.3M));
            _repository.Save(new LedgerReport(Guid.NewGuid(), AccountId, "Action 2", 24.6M));
            _repository.Save(new LedgerReport(Guid.NewGuid(), Guid.NewGuid(), "Action 3", 96.3M));

            var sut = _repository.GetByExample<AccountDetailsReport>(new { AccountName = "Account Name" }).FirstOrDefault();

            Assert.AreEqual(2, sut.Ledgers.Count());
            Assert.AreEqual("Action 1", sut.Ledgers.First().Action);
            Assert.AreEqual(12.3M, sut.Ledgers.First().Amount);
            Assert.AreEqual("Action 2", sut.Ledgers.Last().Action);
            Assert.AreEqual(24.6M, sut.Ledgers.Last().Amount);
        }

        [TestMethod]
        public void Will_be_able_to_update_an_already_saved_dto()
        {
            Guid guid = Guid.NewGuid();
            _repository.Save(new ClientReport(guid, "Mark Nijhof"));

            _repository.Update<ClientReport>(new { Name = "Mark Albert Nijhof" }, new { Id = guid });

            var sut = _repository.GetByExample<ClientReport>(new { Id = guid });

            Assert.AreEqual(1, sut.Count());
            Assert.AreEqual("Mark Albert Nijhof", sut.First().Name);
        }

        [TestMethod]
        public void Will_be_able_to_delete_an_already_saved_dto()
        {
            Guid guid = Guid.NewGuid();
            _repository.Save(new ClientReport(guid, "Mark Nijhof"));

            _repository.Delete<ClientReport>(new { Id = guid });

            var sut = _repository.GetByExample<ClientReport>(new { Id = guid });

            Assert.AreEqual(0, sut.Count());
        }
    }
}