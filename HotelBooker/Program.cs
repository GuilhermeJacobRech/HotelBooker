using HotelBooker.Utils;
using HotelBooker.Repositories;
using HotelBooker.Models;
namespace HotelBooker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Reads .json files as string
            string hotelsJson = Helpers.ReadJsonFile("hotels");
            string bookingsJson = Helpers.ReadJsonFile("bookings");

            // Instantiate repositories. They are responsible for deserializing the json string
            IHotelRepository hotelRepository = new JsonHotelRepository(hotelsJson);
            IBookingRepository bookingRepository = new JsonBookingRepository(bookingsJson);

            List<Hotel> hotels = hotelRepository.GetAll();
            if (hotels.Count > 0)
            {
                // Print list of hotels 
                Helpers.PrintHotelsWithDetails(hotels);

                // Instantiate booking service that handles the logic for bookings
                BookingService bookingService = new BookingService(hotelRepository, bookingRepository);

                while (true)
                {
                    Console.WriteLine("Usage example: Availability(H1, 20240901-20240903, DBL)");

                    // Get and deserialize user input
                    string userInputString = Helpers.GetUserInput();
                    UserInput? parsedInput = Helpers.DeserializeInput(userInputString);

                    // Validates user input
                    if (bookingService.IsUserInputValid(parsedInput))
                    {
                        // Print amount of available rooms
                        int availableRooms = bookingService.GetAvailableRoomsCount(parsedInput.HotelId, parsedInput.Arrival, parsedInput.Departure, parsedInput.RoomTypeCode);
                        Console.WriteLine($"Total available rooms: {availableRooms}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid user input");
                    }
                }
            }

            if (hotels.Count == 0)
            {
                Console.WriteLine("No hotels found in the hotels.json file");
                Console.ReadLine();
                return;
            }
        }
    }
}
