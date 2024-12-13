using System.Text.Json.Serialization;

namespace HotelBooker.Models
{
    public class RoomType
    {
        [JsonPropertyName("code")]
        public required string Code { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("amenities")]
        public List<string> Amenities { get; set; } = [];

        [JsonPropertyName("features")]
        public List<string> Features { get; set; } = [];
    }
}
