using HotelBooker.Models;
using System.Text.Json;

namespace HotelBooker.Repositories
{
    public class JsonBookingRepository : IBookingRepository
    {
        private readonly List<Booking> _bookings;
        private static readonly JsonSerializerOptions s_readOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public JsonBookingRepository(string bookingsJson)
        {
            _bookings = JsonSerializer.Deserialize<List<Booking>>(bookingsJson, s_readOptions) ?? [];
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
