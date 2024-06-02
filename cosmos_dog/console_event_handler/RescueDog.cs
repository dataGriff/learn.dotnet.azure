using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace dog_adopter.Models
{
    public class RescueDog
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        // [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("breed")]
        public string Breed { get; set; }

        // [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}


// [JsonConverter(typeof(JsonStringEnumConverter))]
// public enum Breed
// {
//     Beagle,
//     Boxer,
//     Bulldog,
//     Chihuahua,
//     Dalmatian,
//     GermanShepherd,
//     GoldenRetriever,
//     GreatDane,
//     LabradorRetriever,
//     Poodle,
//     Rottweiler,
//     SiberianHusky,
//     YorkshireTerrier
// }


// [JsonConverter(typeof(JsonStringEnumConverter))]
// public enum Status
// {
//     Adopted,
//     Available,
//     Fostered,
//     Reserved
// }