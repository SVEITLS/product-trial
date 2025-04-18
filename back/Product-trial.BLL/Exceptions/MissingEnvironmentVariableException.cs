using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_trial.BLL.Exceptions
{
    public class MissingEnvironmentVariableException : Exception
    {
        public MissingEnvironmentVariableException() : base("Missing environment variable")
        {
        }

        public MissingEnvironmentVariableException(string key) : base($"The '{key}' environment variable is missing")
        {
        }

        public MissingEnvironmentVariableException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
