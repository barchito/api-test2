using System;
using System.Collections.Generic;
using System.Text;

namespace Core2APIExercise.Data.Entities
{
    /// <summary>
    /// Person Class
    /// </summary>
    public class Person : BaseEntity
    {
        public Person()
        {

        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Identifier> Identifiers { get; set; }
    }
}
