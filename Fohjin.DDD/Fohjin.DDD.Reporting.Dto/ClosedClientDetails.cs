using System;

namespace Fohjin.DDD.Reporting.Dto
{
    public class ClosedClientDetails : ClientDetails
    {
        public ClosedClientDetails(Guid id, string clientName, string street, string streetNumber, string postalCode, string city, string phoneNumber) : base(id, clientName, street, streetNumber, postalCode, city, phoneNumber)
        {
        }
    }
}