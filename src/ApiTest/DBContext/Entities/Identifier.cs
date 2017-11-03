using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTest.DBContext.Entities
{
    public class Identifier
    {
        [Key]
        public Guid Id { get; set; }
        public IdentifierTypes Type { get; set; }
        public string Value { get; set; }
        public Guid PersonId { get; set; }
        [ForeignKey("PersonId")]
        public Person Person { get; set; }
    }

    public enum IdentifierTypes
    {
        Email,
        AccessCard,
        License,
        Plate
    }
}
