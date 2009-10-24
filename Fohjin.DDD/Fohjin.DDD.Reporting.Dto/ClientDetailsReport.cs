using System;
using System.Collections.Generic;

namespace Fohjin.DDD.Reporting.Dto
{
    public class ClientDetailsReport
    {
        public Guid Id { get; private set; }
        public IEnumerable<AccountReport> Accounts { get; private set; }
        public IEnumerable<ClosedAccountReport> ClosedAccounts { get; private set; }
        public string ClientName { get; private set; }
        public string Street { get; private set; }
        public string StreetNumber { get; private set; }
        public string PostalCode { get; private set; }
        public string City { get; private set; }
        public string PhoneNumber { get; set; }

        public ClientDetailsReport(Guid id, string clientName, string street, string streetNumber, string postalCode, string city, string phoneNumber)
        {
            Id = id;
            Accounts = new List<AccountReport>();
            ClosedAccounts = new List<ClosedAccountReport>();
            ClientName = clientName;
            Street = street;
            StreetNumber = streetNumber;
            PostalCode = postalCode;
            City = city;
            PhoneNumber = phoneNumber;
        }
    }
}