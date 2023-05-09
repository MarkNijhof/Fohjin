namespace Fohjin.DDD.Domain.Client
{
    public class PhoneNumber
    {
        public string Number { get; init; }

        public PhoneNumber(string number)
        {
            Number = number;
        }
    }
}