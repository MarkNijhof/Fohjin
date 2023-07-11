namespace Fohjin.DDD.Domain.Account
{
    public class Balance
    {
        private readonly Amount _amount = new(0);

        public Balance() { }
        private Balance(decimal decimalAmount) => _amount = new(decimalAmount);

        public Balance Withdrawal(Amount amount) => new(_amount.Substract(amount));
        public Balance Deposit(Amount amount) => new(_amount.Add(amount));

        public bool WithdrawalWillResultInNegativeBalance(Amount amount) =>
            new Amount(_amount).Substract(amount).IsNegative();

        public static implicit operator decimal(Balance balance) => balance._amount;
        public static implicit operator Balance(decimal decimalAmount) => new (decimalAmount);
    }
}