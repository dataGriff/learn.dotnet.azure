using System;
//using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace dog_adopter.Models
{
    public class RescueDog
    {
        public RescueDog(string name, Breed breed, Status status)
        {
            Name = name;
            Breed = breed;
            Status = status;
            Id = Guid.NewGuid();
            Timestamp = DateTime.UtcNow;
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; }

        [JsonProperty("breed")]
        public Breed Breed { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; }
    }
}

 [JsonConverter(typeof(StringEnumConverter))]
public enum Breed
{
    Beagle,
    Boxer,
    Bulldog,
    Chihuahua,
    Dalmatian,
    GermanShepherd,
    GoldenRetriever,
    GreatDane,
    LabradorRetriever,
    Poodle,
    Rottweiler,
    SiberianHusky,
    YorkshireTerrier
}

[JsonConverter(typeof(StringEnumConverter))]
public enum Status
{
    Adopted,
    Available,
    Fostered,
    Reserved
}
