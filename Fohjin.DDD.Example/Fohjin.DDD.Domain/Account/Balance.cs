namespace Fohjin.DDD.Domain.Account
{
    public class Balance
    {
        private readonly Amount _amount;

        public Balance()
        {
            _amount = new Amount(0);
        }

        private Balance(decimal decimalAmount)
        {
            _amount = new Amount(decimalAmount);
        }

        public Balance Withdrawal(Amount amount)
        {
            return new Balance(_amount.Substract(amount));
        }

        public Balance Deposit(Amount amount)
        {
            return new Balance(_amount.Add(amount));
        }

        public bool WithdrawalWillResultInNegativeBalance(Amount amount)
        {
            return new Amount(_amount).Substract(amount).IsNegative();
        }

        public static implicit operator decimal(Balance balance)
        {
            return balance._amount;
        }

        public static implicit operator Balance(decimal decimalAmount)
        {
            return new Balance(decimalAmount);
        }
    }
}