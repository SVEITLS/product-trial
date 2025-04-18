namespace Product_trial.Auth
{
    public class AccountNotFoundException : Exception
    {
        public AccountNotFoundException() : base("Account not found")
        {
        }

        public AccountNotFoundException(string email) : base($"The account with email : {email} does not exist")
        {
        }

        public AccountNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
