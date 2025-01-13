using HotelBooker.Models;
using HotelBooker.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace HotelBooker
{
    public class BookingService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IHotelRepository hotelRepository, IBookingRepository bookingRepository)
        {
            _hotelRepository = hotelRepository;
            _bookingRepository = bookingRepository;
        }

        /// <summary>
        /// Calculates the amount of total rooms in a hotel for a specific room type
        /// and returns the result of subtracting by how many of those rooms are already booked.
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="arrivalToCheck"></param>
        /// <param name="departureToCheck"></param>
        /// <param name="roomTypeCode"></param>
        public int GetAvailableRoomsCount(string hotelId, DateTime arrivalToCheck, DateTime departureToCheck, string roomTypeCode)
        {
            var totalRoomsCount = _hotelRepository.GetRoomsByType(hotelId, roomTypeCode)?.Count ?? 0;

            var overlapRoomsCount = _bookingRepository.GetBookingsByHotelId(hotelId)
                .Where(booking => booking.RoomType == roomTypeCode &&
                (booking.Arrival < departureToCheck && booking.Departure > arrivalToCheck))
                .Count();

            return totalRoomsCount - overlapRoomsCount;
        }
    }
}


