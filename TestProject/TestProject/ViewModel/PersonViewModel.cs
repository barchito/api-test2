using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Enum;

namespace TestProject.ViewModel
{
    /// <summary>
    /// Person
    /// </summary>
    public class PersonViewModel
    {
        /// <summary>
        /// FirstName
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Collection of Identifiers.
        /// </summary>
        public IEnumerable<IdentifiersViewModel> Identifiers { get; set; }
    }
}
