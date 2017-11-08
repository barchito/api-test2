using AdvancedStudioExercise.Web.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AdvancedStudioExercise.Web.Models {
    public class ApiResponseModel {
        public ApiResponseModel () {
            Status = Status.FAILED;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}