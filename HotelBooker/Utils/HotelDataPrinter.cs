using HotelBooker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooker.Utils
{
    internal class HotelDataPrinter
    {
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
                TablePrinter.PrintTable(roomTypesHeader, roomTypesRows);

                // New line to separate each hotel
                Console.WriteLine();
            }
        }
    }
}
