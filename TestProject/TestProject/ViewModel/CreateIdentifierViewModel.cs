using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Enum;

namespace TestProject.ViewModel
{
    /// <summary>
    /// Create a New Idetifier.
    /// </summary>
    public class CreateIdentifierViewModel
    {

        /// <summary>
        /// 
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        public IdentifiersType Type { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public string Value { get; set; }

    }
}
