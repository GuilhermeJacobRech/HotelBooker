using HotelBooker.Models;
using System.Text.Json;

namespace HotelBooker.Repositories
{
    public class JsonBookingRepository : IBookingRepository
    {
        private readonly List<Booking> _bookings;
        public JsonBookingRepository(string bookingsJson)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _bookings = JsonSerializer.Deserialize<List<Booking>>(bookingsJson, options) ?? [];
        }

        /// <summary>
        /// Returns all bookings for a specific hotel
        /// </summary>
        /// <param name="hotelId"></param>
        public List<Booking> GetBookingsByHotelId(string hotelId)
        {
            return _bookings.Where(x => x.HotelId == hotelId).ToList();
        }
    }
}
