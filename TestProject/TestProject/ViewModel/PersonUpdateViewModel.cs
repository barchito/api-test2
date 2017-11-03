using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.ViewModel
{
    /// <summary>
    /// Person Update View Model
    /// </summary>
    public class PersonUpdateViewModel
    {
        /// <summary>
        /// PersonId
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// FirstName
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName { get; set; }
    }
}
