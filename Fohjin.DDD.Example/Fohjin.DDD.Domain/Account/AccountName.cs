namespace Fohjin.DDD.Domain.Account
{
    public class AccountName
    {
        public string Name { get; private set; }

        public AccountName(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}