namespace Product_trial.Auth
{
    public class AccountCredentialsException : Exception
    {
        public AccountCredentialsException() : base("Account credentials incorrect")
        {
        }

        public AccountCredentialsException(string email) : base($"The credentials for account {email} are incorrect")
        {
        }

        public AccountCredentialsException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
