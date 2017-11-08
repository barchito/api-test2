using System.Collections.Generic;
using AdvancedStudioExercise.Infrastructure.Sql.Entities.Base;

namespace AdvancedStudioExercise.Infrastructure.Sql.Entities {
    public class Person : EntityBase {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Identifier> Identifiers { get; set; }
    }
}