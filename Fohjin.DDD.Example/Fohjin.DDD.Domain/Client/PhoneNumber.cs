namespace Fohjin.DDD.Domain.Client
{
    public class PhoneNumber
    {
        public string Number { get; private set; }

        public PhoneNumber(string number)
        {
            Number = number;
        }
    }
}