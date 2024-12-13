using System.Text.Json;
using System.Text.Json.Serialization;

namespace HotelBooker.Utils
{
    // Custom JsonConverter implementation for serializing and deserializing DateTime objects to and from JSON, using a specified date format
    // Taken from https://learn.microsoft.com/en-us/dotnet/standard/datetime/system-text-json-support#-and-
    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        // The expected date format for serialization and deserialization
        private const string DateFormat = "yyyyMMdd";

        // Deserializes a JSON string into a DateTime object
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dateString = reader.GetString();

            if (DateTime.TryParseExact(dateString, DateFormat, null, System.Globalization.DateTimeStyles.None, out var date))
            {
                return date;
            }

            throw new JsonException($"Invalid date format. Expected format is {DateFormat}.");
        }

        // Serializes a DateTime object into a JSON string using the specified format
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(DateFormat));
        }
    }
}
