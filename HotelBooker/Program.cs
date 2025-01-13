using HotelBooker.Utils;
using HotelBooker.Repositories;
using HotelBooker.Models;
namespace HotelBooker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string hotelsJson = JsonFileReader.ReadJsonFile("hotels");
            string bookingsJson = JsonFileReader.ReadJsonFile("bookings");

            IHotelRepository hotelRepository = new JsonHotelRepository(hotelsJson);
            IBookingRepository bookingRepository = new JsonBookingRepository(bookingsJson);

            List<Hotel> hotels = hotelRepository.GetAll();
            if (hotels.Count > 0)
            {
                HotelDataPrinter.PrintHotelsWithDetails(hotels);

                BookingService bookingService = new(hotelRepository, bookingRepository);
                UserInputHandler userInputHandler = new(hotelRepository);

                // Only way to exit the application is by the user entering a null or blank input.
                while (true)
                {
                    Console.WriteLine("Usage example: Availability(H1, 20240901-20240903, DBL)");

                    string userInputString = UserInputHandler.GetUserInput();
                    UserInput? parsedInput = UserInputHandler.DeserializeInput(userInputString);

                    if (userInputHandler.IsUserInputValid(parsedInput))
                    {
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
