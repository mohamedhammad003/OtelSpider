using OtelSpider.Core.BLL.Repositories;
using OtelSpider.Core.DAL.Models;
using OtelSpider.Core.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace OtelSpider.Core.BLL.Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository roomTypeRepository;
        private readonly IRoomMealPlanRepository roomMealPlanRepository;
        private readonly IUnitOfWork unitOfWork;
        public RoomTypeService(IRoomTypeRepository roomTypeRepository
            , IRoomMealPlanRepository roomMealPlanRepository
            , IUnitOfWork unitOfWork)
        {
            this.roomTypeRepository = roomTypeRepository;
            this.roomMealPlanRepository = roomMealPlanRepository;
            this.unitOfWork = unitOfWork;
        }
        #region Room Type
        public void Create(RoomType item)
        {
            if (item.IsBase)
            {
                var hotelRooms = getBaseRooms(item.HotelID, 0);
                foreach (var room in hotelRooms)
                {
                    room.IsBase = false;
                    roomTypeRepository.Update(room);
                }
            }
            roomTypeRepository.Add(item);
        }
        public void Update(RoomType item)
        {
            if (item.IsBase)
            {
                var hotelRooms = getBaseRooms(item.HotelID, item.ID);
                foreach (var room in hotelRooms)
                {
                    room.IsBase = false;
                    roomTypeRepository.Update(room);
                }
            }
            roomTypeRepository.Update(item);
        }
        public void Delete(int id)
        {
            var item = GetRoomType(id);
            roomTypeRepository.Delete(item);
        }
        public RoomType GetRoomType(int id)
        {
            return roomTypeRepository.Get(m => m.ID == id);
        }
        public RoomType GetRoomType(string type)
        {
            return roomTypeRepository.Get(m => m.Type == type);
        }
        public IEnumerable<RoomType> GetRoomTypes()
        {
            return roomTypeRepository.GetMany(r => true);
        }
        public IEnumerable<RoomType> GetRoomTypesByHotelID(int hotelID)
        {
            return roomTypeRepository.GetMany(r => r.HotelID == hotelID);
        }
        public IEnumerable<RoomType> GetRoomTypesByUserID(string systemUserID)
        {
            return roomTypeRepository.GetRoomTypesByUserID(systemUserID);
        }
        public IEnumerable<RoomType> GetRoomTypesByHotels(List<int> hotelIds)
        {
            return roomTypeRepository.GetMany(r => hotelIds.Contains(r.HotelID));
        }
        public void updateBaseRoom(int hotelId, int baseRoomId = 0)
        {
            var hotelRooms = GetRoomTypesByHotelID(hotelId);
            foreach (var room in hotelRooms)
            {
                if (room.ID != baseRoomId)
                {
                    room.IsBase = false;
                    roomTypeRepository.Update(room);
                }
            }
        }
        private IEnumerable<RoomType> getBaseRooms(int hotelId, int baseRoomId)
        {
            return roomTypeRepository.GetMany(r => r.HotelID == hotelId && r.ID != baseRoomId && r.IsBase == true);
        }
        #endregion
        #region Room Meal Plans
        public void AddRoomMealPlan(RoomMealPlan item)
        {
            roomMealPlanRepository.Add(item);
        }
        public void UpdateRoomMealPlan(RoomMealPlan item)
        {
            roomMealPlanRepository.Update(item);
        }
        public void DeleteRoomMealPlan(int id)
        {
            var item = GetRoomMealPlan(id);
            roomMealPlanRepository.Delete(item);
        }

        public void DeleteRoomMealPlans(int roomId)
        {
            foreach (var item in GetRoomMealPlans(roomId))
            {
                roomMealPlanRepository.Delete(item);
            }
        }
        public RoomMealPlan GetRoomMealPlan(int id)
        {
            return roomMealPlanRepository.Get(m => m.ID == id);
        }
        public RoomMealPlan GetRoomMealPlan(int roomTypeId, int mealPlanId)
        {
            return roomMealPlanRepository.Get(m => m.RoomTypeID == roomTypeId && m.MealPlanID == mealPlanId);
        }
        public IEnumerable<RoomMealPlan> GetRoomMealPlans(int roomTypeId)
        {
            return roomMealPlanRepository.GetMany(m => m.RoomTypeID == roomTypeId);
        }
        #endregion
        public void SaveChanges()
        {
            unitOfWork.Commit();
        }
    }
}
