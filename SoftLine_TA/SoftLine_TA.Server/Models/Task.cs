using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace SoftLine_TA.Models
{
    [JsonObject]
    public class Task
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("statusId")]
        public int Status_ID { get; set; }
        public virtual Status Status { get; set; }
    }
}
