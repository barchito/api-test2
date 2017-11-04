using System;
using System.Collections.Generic;
using System.Text;
using Core2APIExercise.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core2APIExercise.Data.Entities
{
    /// <summary>
    /// Identifiers for person
    /// </summary>
    public class Identifier : BaseEntity
    {
        public Identifier()
        {
            CreatedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
        }

         [ForeignKey("Person")]
         public Guid PersonId { get; set; }
        public Person Person { get; set; }

        public IdentifierType IdentifierType { get; set; }
        public string Value { get; set; }
    }
}
