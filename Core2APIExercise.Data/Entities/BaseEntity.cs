using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core2APIExercise.Data.Entities
{
    /// <summary>
    /// Base class that hold common properties on Entity
    /// </summary>
    public abstract class BaseEntity
    {
        public Guid ID { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
