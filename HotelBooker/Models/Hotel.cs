using System.Text.Json.Serialization;

namespace HotelBooker.Models
{
    public class Hotel
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }

        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("roomTypes")]
        public required List<RoomType> RoomTypes { get; set; }

        [JsonPropertyName("rooms")]
        public required List<Room> Rooms { get; set; }
    }
}
