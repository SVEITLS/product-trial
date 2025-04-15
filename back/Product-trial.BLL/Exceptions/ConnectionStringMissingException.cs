namespace Product_trial.BLL.Exceptions
{
    public class ConnectionStringMissingException : Exception
    {
        public ConnectionStringMissingException() : base("Connection string is missing")
        {
        }

        public ConnectionStringMissingException(string connectionStringKey) : base($"The connection string for '{connectionStringKey}' is missing")
        {
        }

        public ConnectionStringMissingException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
