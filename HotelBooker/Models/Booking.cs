using HotelBooker.Utils;
using System.Text.Json.Serialization;

namespace HotelBooker.Models
{
    public class Booking
    {
        [JsonPropertyName("hotelId")]
        public required string HotelId { get; set; }

        [JsonPropertyName("arrival")]
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime Arrival { get; set; }

        [JsonPropertyName("departure")]
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime Departure { get; set; }

        [JsonPropertyName("roomType")]
        public required string RoomType { get; set; }

        [JsonPropertyName("roomRate")]
        public string? RoomRate { get; set; }
    }
}
