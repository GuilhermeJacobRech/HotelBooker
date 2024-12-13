using HotelBooker.Models;
using HotelBooker.Repositories;

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
        /// Validates user input.
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns></returns>
        public bool IsUserInputValid(UserInput? userInput)
        {
            if (userInput is not null)
            {
                // Check if hotel exists
                var hotel = _hotelRepository.GetHotelById(userInput.HotelId);
                if (hotel is not null)
                {
                    // Check if room type exists in hotel
                    var roomType = _hotelRepository.GetRoomType(userInput.HotelId, userInput.RoomTypeCode);
                    if (roomType is not null)
                    {
                        // Check if arrival is earlier or equal than departure
                        if (userInput.Arrival <= userInput.Departure)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
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


