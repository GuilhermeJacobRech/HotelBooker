using HotelBooker.Models;

namespace HotelBooker.Repositories
{
    public interface IHotelRepository
    {
        public List<Hotel> GetAll();
        public Hotel? GetHotelById(string id);
        public RoomType? GetRoomType(string hotelId, string roomTypeCode);

        public List<Room>? GetRoomsByType(string hotelId, string roomTypeCode);

    }
}
