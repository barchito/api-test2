using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Enum;

namespace TestProject.Entites
{
    /// <summary>
    /// Identifiers Entity
    /// </summary>
    public class Identifier : BaseEntity
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

        /// <summary>
        /// Person
        /// </summary>
        public virtual Person Person { get; set; }
    }
}
