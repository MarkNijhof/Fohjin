using System;
using System.Collections.Generic;
using Fohjin.DDD.Domain.Entities.Mementos;
using Fohjin.DDD.Domain.ValueObjects;

namespace Fohjin.DDD.Domain.Entities
{
    public class Client : BaseAggregateRoot, IOrginator
    {
        private PhoneNumber _phoneNumber;
        private Address _address;
        private Name _name;
        private readonly List<Guid> _accounts;

        public Client()
        {
            _accounts = new List<Guid>();
        }

        public void CreateClient(Name name, Address address, PhoneNumber phoneNumber)
        {
            
        }

        public void UpdatePhoneNumber(PhoneNumber phoneNumber)
        {
            
        }

        public void ClientMoved(Address newAddress)
        {
            
        }

        public void AddAccount(Guid accountId)
        {
            
        }

        public void RemoveAccount(Guid accountId)
        {
            
        }

        public IMemento CreateMemento()
        {
            throw new NotImplementedException();
        }

        public void SetMemento(IMemento memento)
        {
            throw new NotImplementedException();
        }
    }
}