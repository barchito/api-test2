using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.Entites
{
    /// <summary>
    /// Person
    /// </summary>
    public class Person : BaseEntity
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
        /// Is Deleted.
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Collection of Identifiers.
        /// </summary>
        public virtual ICollection<Identifier> Identifiers { get; set; }
    }
}
