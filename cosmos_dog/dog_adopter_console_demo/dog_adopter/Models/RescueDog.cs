using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace dog_adopter.Models
{
    public class RescueDog
    {
        public RescueDog(string name, Breed breed, Status status, Guid id, DateTime timestamp)
        {
            Name = name;
            Breed = breed;
            Status = status;
            Id = id;
            Timestamp = timestamp;
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set;}

        [JsonProperty("breed")]
        public Breed Breed { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set;}
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