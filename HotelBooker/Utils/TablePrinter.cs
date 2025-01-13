using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooker.Utils
{
    internal class TablePrinter
    {
        public static void PrintTable(List<string> headers, List<List<string>> rows)
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
