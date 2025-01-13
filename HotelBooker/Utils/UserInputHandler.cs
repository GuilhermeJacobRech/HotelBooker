using HotelBooker.Models;
using HotelBooker.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HotelBooker.Utils
{
    public class UserInputHandler
    {
        private readonly IHotelRepository _hotelRepository;

        public UserInputHandler(IHotelRepository hotelRepository)
        {
            this._hotelRepository = hotelRepository;
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
        /// Get user input. If input is null or empty the application will exit with status 0.
        /// </summary>
        /// <returns>Input inserted by user</returns>
        public static string GetUserInput()
        {
            string? input = Console.ReadLine();

            // Program exit if the user enters a blank line
            if (string.IsNullOrEmpty(input)) Environment.Exit(0);

            return input;
        }

        /// <summary>
        /// Deserializes user input. Returns null if regex doesnt match sucessfully or fail to parse arrival and departure datetime.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static UserInput? DeserializeInput(string input)
        {
            var pattern = @"Availability\((H\d+), (\d{8})-(\d{8}), (\w+)\)";
            var match = Regex.Match(input, pattern);

            if (match.Success)
            {
                if (DateTime.TryParseExact(match.Groups[2].Value, "yyyyMMdd", null, DateTimeStyles.None, out DateTime arrival) &&
                    DateTime.TryParseExact(match.Groups[3].Value, "yyyyMMdd", null, DateTimeStyles.None, out DateTime departure))
                {
                    return new UserInput
                    {
                        HotelId = match.Groups[1].Value,
                        Arrival = arrival,
                        Departure = departure,
                        RoomTypeCode = match.Groups[4].Value
                    };
                }
            }
            return null;
        }
    }
}
