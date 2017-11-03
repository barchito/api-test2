using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.Exceptions
{
    /// <summary>
    /// This exception should be thrown when user input is invalid or the request is bad
    /// </summary>
    public class FailException : Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message"></param>
        public FailException(string message) : base(message)
        {
        }
    }
}
