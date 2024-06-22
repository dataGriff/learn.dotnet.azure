using System;
using System.Text.Json.Serialization;

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

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("id")]
        public Guid Id { get; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("breed")]
        public Breed Breed { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("status")]
        public Status Status { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; }
    }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
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

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Status
{
    Adopted,
    Available,
    Fostered,
    Reserved
}
