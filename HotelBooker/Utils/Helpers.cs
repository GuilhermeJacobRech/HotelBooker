using HotelBooker.Models;
using System.Globalization;
using System.Text.RegularExpressions;

namespace HotelBooker.Utils
{
    public static class Helpers
    {
        /// <summary>
        /// Read .json file located in the same directory of this application.
        /// </summary>
        /// <param name="filename">Name of the file without extensions.</param>
        /// <returns>File content as string</returns>
        /// <exception cref="Exception"></exception>
        public static string ReadJsonFile(string filename)
        {
            // Check if the file exists in the base directory
            if (File.Exists(AppContext.BaseDirectory + $"{filename}.json"))
            {
                string json = File.ReadAllText(AppContext.BaseDirectory + $"{filename}.json");
                return json;
            }

            throw new Exception($"Unable to locate {filename}.json. Please make sure that the file exists in the same directory ({AppContext.BaseDirectory}) of this application.");
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
                DateTime arrival;
                DateTime departure;

                if (DateTime.TryParseExact(match.Groups[2].Value, "yyyyMMdd", null, DateTimeStyles.None, out arrival) &&
                    DateTime.TryParseExact(match.Groups[3].Value, "yyyyMMdd", null, DateTimeStyles.None, out departure))
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

        /// <summary>
        /// Print list of hotels with every room type and its amenities and features.
        /// </summary>
        /// <param name="hotels"></param>
        public static void PrintHotelsWithDetails(List<Hotel> hotels)
        {
            foreach (var hotel in hotels)
            {
                Console.WriteLine($"Name: {hotel.Name} - Id: {hotel.Id}");

                // Prepare table headers and rows for room types
                var roomTypesHeader = new List<string> { "Code", "Description", "Amenities", "Features" };
                var roomTypesRows = new List<List<string>>();

                foreach (var rt in hotel.RoomTypes)
                {
                    roomTypesRows.Add(new List<string>
                    {
                        rt.Code,
                        rt.Description,
                        string.Join(" - ", rt.Amenities),
                        string.Join(" - ", rt.Features)
                    });
                }

                // Print room types table
                PrintTable(roomTypesHeader, roomTypesRows);

                // New line to separate each hotel
                Console.WriteLine();
            }
        }

        private static void PrintTable(List<string> headers, List<List<string>> rows)
        {
            // Determine column widths
            var columnWidths = new List<int>();
            for (int i = 0; i < headers.Count; i++)
            {
                int maxWidth = headers[i].Length;
                foreach (var row in rows)
                {
                    if (i < row.Count) // Ensure the row has enough columns
                        maxWidth = Math.Max(maxWidth, row[i].Length);
                }
                columnWidths.Add(maxWidth);
            }

            // Print header
            PrintSeparator(columnWidths);
            PrintRow(headers, columnWidths);
            PrintSeparator(columnWidths);

            // Print rows
            foreach (var row in rows)
            {
                PrintRow(row, columnWidths);
            }

            // Print footer
            PrintSeparator(columnWidths);
        }

        private static void PrintSeparator(List<int> columnWidths)
        {
            Console.Write("+");
            foreach (var width in columnWidths)
            {
                Console.Write(new string('-', width + 2));
                Console.Write("+");
            }
            Console.WriteLine();
        }

        private static void PrintRow(List<string> row, List<int> columnWidths)
        {
            Console.Write("|");
            for (int i = 0; i < columnWidths.Count; i++)
            {
                string cell = i < row.Count ? row[i] : ""; // Handle cases where row has fewer columns
                Console.Write(" " + cell.PadRight(columnWidths[i]) + " |");
            }
            Console.WriteLine();
        }
    }
}
