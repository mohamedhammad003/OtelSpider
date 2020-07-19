using OtelSpider.Core.DAL.Models;
using System.Collections.Generic;

namespace OtelSpider.Core.BLL.Services
{
    public interface IRoomTypeService
    {
        #region Room Type
        void Create(RoomType item);
        void Update(RoomType item);
        void Delete(int id);
        RoomType GetRoomType(int id);
        RoomType GetRoomType(string type);
        IEnumerable<RoomType> GetRoomTypes();
        IEnumerable<RoomType> GetRoomTypesByHotelID(int hotelID);
        IEnumerable<RoomType> GetRoomTypesByUserID(string systemUserID);
        IEnumerable<RoomType> GetRoomTypesByHotels(List<int> hotelIds);
        void updateBaseRoom(int hotelId, int baseRoomId = 0);
        #endregion

        #region Room Meal Plan
        void AddRoomMealPlan(RoomMealPlan item);
        void UpdateRoomMealPlan(RoomMealPlan item);
        void DeleteRoomMealPlan(int id);
        void DeleteRoomMealPlans(int roomId);
        RoomMealPlan GetRoomMealPlan(int id);
        RoomMealPlan GetRoomMealPlan(int roomTypeId, int mealPlanId);
        IEnumerable<RoomMealPlan> GetRoomMealPlans(int roomTypeId);
        #endregion
        void SaveChanges();
    }
}
