using System;
using System.Collections.Generic;
using System.Text;

namespace Core2APIExercise.Data.Models
{
    /// <summary>
    /// Person Model class 
    /// </summary>
    public class PersonModel
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class PersonIndentifierModel
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual IEnumerable<IdentifierModel> Identifiers { get; set; }
    }
}
