using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Distillery
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } 

        [JsonPropertyName("wikiLink")]
        public string? WikiLink { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }
}