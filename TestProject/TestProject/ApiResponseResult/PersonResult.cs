using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Enum;

namespace TestProject.ApiResponseResult
{

    #region PersonWithIdentfiersResult
    /// <summary>
    /// Person Result.
    /// </summary>
    public class PersonWithIdentfiersResult
    {
        /// <summary>
        /// Person Unique Id.
        /// </summary>
        public Guid Id { get; set; }

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
        public IEnumerable<IdentifierResult> Identifiers { get; set; }
    }
    #endregion

    #region IdentifierResult

    /// <summary>
    /// IdentifierResult
    /// </summary>
    public class IdentifierResult
    {
        /// <summary>
        /// Identifier Unique Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        public IdentifiersType Type { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public string Value { get; set; }
    }
    #endregion
}
