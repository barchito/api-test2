using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.Exceptions
{
    /// <summary>
    /// This exception should be thrown when there is an internal server error
    /// </summary>
    public class ErrorException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        public ErrorException(string message) : base(message)
        {

        }
    }
}
