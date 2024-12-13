namespace HotelBooker.Models
{
    public class UserInput
    {
        public required string HotelId { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }
        public required string RoomTypeCode { get; set; }
    }
}
