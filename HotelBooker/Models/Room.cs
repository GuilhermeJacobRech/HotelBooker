using System.Text.Json.Serialization;
namespace HotelBooker.Models
{
    public class Room
    {
        [JsonPropertyName("roomType")]
        public required string RoomType { get; set; }

        [JsonPropertyName("roomId")]
        public string RoomId { get; set; } = string.Empty;
    }
}
