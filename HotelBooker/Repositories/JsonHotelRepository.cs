using HotelBooker.Models;
using System.Text.Json;

namespace HotelBooker.Repositories
{
    public class JsonHotelRepository : IHotelRepository
    {
        private readonly List<Hotel> _hotels;
        private static readonly JsonSerializerOptions s_readOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public JsonHotelRepository(string hotelsJson)
        {
            _hotels = JsonSerializer.Deserialize<List<Hotel>>(hotelsJson, s_readOptions) ?? [];
        }

        public List<Hotel> GetAll()
        {
            return _hotels;
        }

        public Hotel? GetHotelById(string id)
        {
            return _hotels.FirstOrDefault(x => x.Id == id);
        }

        public RoomType? GetRoomType(string hotelId, string roomTypeCode)
        {
            return _hotels.FirstOrDefault(x => x.Id == hotelId)?
                .RoomTypes.FirstOrDefault(x => x.Code == roomTypeCode);
        }

        public List<Room>? GetRoomsByType(string hotelId, string roomTypeCode)
        {
            return _hotels.FirstOrDefault(x => x.Id == hotelId)?
                .Rooms.Where(x => x.RoomType == roomTypeCode).ToList();
        }
    }
}
