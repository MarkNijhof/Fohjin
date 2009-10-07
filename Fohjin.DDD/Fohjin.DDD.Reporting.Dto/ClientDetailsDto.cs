using System;
using System.Collections.Generic;

namespace Fohjin.DDD.Reporting.Dto
{
    public class ClientDetailsDto
    {
        public Guid Id { get; private set; }
        public IEnumerable<AccountDto> Accounts { get; private set; }
        public string ClientName { get; private set; }
        public string Street { get; private set; }
        public string StreetNumber { get; private set; }
        public string PostalCode { get; private set; }
        public string City { get; private set; }

        public ClientDetailsDto(Guid id, IEnumerable<AccountDto> accounts, string clientName, string street, string streetNumber, string postalCode, string city)
        {
            Id = id;
            Accounts = accounts;
            ClientName = clientName;
            Street = street;
            StreetNumber = streetNumber;
            PostalCode = postalCode;
            City = city;
        }
    }
}