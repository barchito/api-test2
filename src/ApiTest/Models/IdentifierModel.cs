using ApiTest.DBContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTest.Models
{
    public class IdentifierModel
    {
        public Guid Id { get; set; }
        public IdentifierTypes Type { get; set; }
        public string Value { get; set; }
        public Guid PersonId { get; set; }
    }
}
