using Core2APIExercise.Data.Enum;
using Newtonsoft.Json;

namespace Core2APIExercise.Data.Models
{
    /// <summary>
    /// Model class on Data Transfer Object
    /// </summary>
    public class IdentifierModel
    {
        public int IdentifierType { get; set; }
        public string Value { get; set; }
        [JsonIgnore]
        public virtual PersonModel Person { get; set; }
    }
}