using HotelBooker.Models;

namespace HotelBooker.Repositories
{
    public interface IBookingRepository
    {
        public List<Booking> GetBookingsByHotelId(string hotelId);
    }
}
