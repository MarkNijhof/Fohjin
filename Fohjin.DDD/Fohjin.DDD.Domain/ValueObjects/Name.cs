namespace Fohjin.DDD.Domain.ValueObjects
{
    public class Name
    {
        public string LastName { get; private set; }
        public string MiddleName { get; private set; }
        public string FirstName { get; private set; }

        public Name(string firstName, string middleName, string lastName)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", string.Format("{0} {1}", FirstName, MiddleName).Trim(), LastName).Trim();
        }
    }
}