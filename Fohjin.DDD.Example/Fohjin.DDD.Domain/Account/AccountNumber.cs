namespace Fohjin.DDD.Domain.Account
{
    public class AccountNumber
    {
        public string Number { get; init; }

        public AccountNumber(string number)
        {
            Number = number;
        }

        public override string ToString()
        {
            return Number;
        }
    }
}