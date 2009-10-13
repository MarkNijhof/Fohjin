using System;
using System.Collections.Generic;

namespace Fohjin.DDD.Reporting.Dto
{
    public class ClientDetails
    {
        public Guid Id { get; private set; }
        public IEnumerable<Account> Accounts { get; private set; }
        public string ClientName { get; private set; }
        public string Street { get; private set; }
        public string StreetNumber { get; private set; }
        public string PostalCode { get; private set; }
        public string City { get; private set; }

        public ClientDetails(Guid id, string clientName, string street, string streetNumber, string postalCode, string city)
        {
            Id = id;
            Accounts = new List<Account>();
            ClientName = clientName;
            Street = street;
            StreetNumber = streetNumber;
            PostalCode = postalCode;
            City = city;
        }
    }
}