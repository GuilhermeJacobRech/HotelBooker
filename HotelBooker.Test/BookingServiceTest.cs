using Moq;
using HotelBooker.Repositories;
using HotelBooker.Models;
namespace HotelBooker.Test
{
    public class BookingServiceTest
    {
        // In a hotel with a single room
        [Theory]
        // Jon Doe arrives day 5 and leaves at 7. You are checking to book from day 8 to 10. The room will be available.
        [InlineData(5, 7, 8, 10, 1)] // Scenario 1: No Overlap

        // Jon Doe arrives day 5 and leaves at 7. You are checking to book from day 6 to 8. No room available
        [InlineData(5, 7, 6, 8, 0)]  // Scenario 2: Partial Overlap at the Start

        // Jon Doe arrives day 5 and leaves at 7. You are checking to book from day 4 to 6. No room available
        [InlineData(5, 7, 4, 6, 0)]  // Scenario 3: Partial Overlap at the End

        // Jon Doe arrives day 5 and leaves at 7. You are checking to book from day 5 to 7. No room available
        [InlineData(5, 7, 5, 7, 0)]  // Scenario 4: Exact Match

        // Jon Doe arrives day 5 and leaves at 10. You are checking to book from day 6 to 8. No room available
        [InlineData(5, 10, 6, 8, 0)] // Scenario 5: Completely Inside

        // Jon Doe arrives day 5 and leaves at 10. You are checking to book from day 4 to 11. No room available
        [InlineData(5, 10, 4, 11, 0)] // Scenario 6: Completely Outside
        public void GetAvailableRoomsCount_TestAllOverlapScenarios(
            int bookingArrivalDay,
            int bookingDepartureDay,
            int arrivalToCheckDay,
            int departureToCheckDay,
            int expectedAvailableRooms)
        {
            // Arrange
            var mockHotelRepository = new Mock<IHotelRepository>();
            var mockBookingRepository = new Mock<IBookingRepository>();

            string hotelId = "H1";
            string roomType = "SGL";

            // Mock only one room in the hotel
            var rooms = new List<Room>
                {
                    new Room { RoomType = roomType }
            };

            mockHotelRepository
                .Setup(repo => repo.GetRoomsByType(hotelId, roomType))
                .Returns(rooms);

            // Mock one booking in that room
            var bookings = new List<Booking>
            {
                new Booking
                {
                    HotelId = hotelId,
                    RoomType = roomType,
                    Arrival = new DateTime(2024, 12, bookingArrivalDay),
                    Departure = new DateTime(2024, 12, bookingDepartureDay)
                }
            };

            mockBookingRepository
                .Setup(repo => repo.GetBookingsByHotelId(hotelId))
                .Returns(bookings);

            var bookingService = new BookingService(mockHotelRepository.Object, mockBookingRepository.Object);

            DateTime arrivalToCheck = new DateTime(2024, 12, arrivalToCheckDay);
            DateTime departureToCheck = new DateTime(2024, 12, departureToCheckDay);

            // Act
            int availableRooms = bookingService.GetAvailableRoomsCount(hotelId, arrivalToCheck, departureToCheck, roomType);

            // Assert
            Assert.Equal(expectedAvailableRooms, availableRooms);
        }

        [Fact]
        public void GetAvailableRoomsCount_TestOverbooking()
        {
            // Arrange
            var mockHotelRepository = new Mock<IHotelRepository>();
            var mockBookingRepository = new Mock<IBookingRepository>();

            string hotelId = "H1";
            string roomType = "SGL";

            // Mock only one room in the hotel
            var rooms = new List<Room>
            {
                new Room { RoomType = roomType }
            };

            mockHotelRepository
                .Setup(repo => repo.GetRoomsByType(hotelId, roomType))
                .Returns(rooms);

            int i = 0;
            var bookings = new List<Booking>();
            // Adds 5 bookings that will overlap
            while (i < 5)
            {
                bookings.Add(
                new Booking
                {
                    HotelId = hotelId,
                    Arrival = new DateTime(2024, 12, 25),
                    Departure = new DateTime(2024, 12, 30),
                    RoomType = roomType
                });

                i++;
            }

            mockBookingRepository
                .Setup(repo => repo.GetBookingsByHotelId(hotelId))
                .Returns(bookings);

            BookingService bookingService = new BookingService(mockHotelRepository.Object, mockBookingRepository.Object);

            DateTime arrivalToCheck = new DateTime(2024, 12, 26);
            DateTime departureToCheck = new DateTime(2024, 12, 28);

            // Act
            int availableRoomsCount = bookingService.GetAvailableRoomsCount(hotelId, arrivalToCheck, departureToCheck, roomType);

            // Assert
            Assert.Equal(-4, availableRoomsCount);
        }

    }
}