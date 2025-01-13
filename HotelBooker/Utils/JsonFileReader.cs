using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooker.Utils
{
    internal class JsonFileReader
    {
        // <summary>
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
    }
}
