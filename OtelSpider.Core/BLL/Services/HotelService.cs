using OtelSpider.Core.BLL.Repositories;
using OtelSpider.Core.DAL.Models;
using OtelSpider.Core.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace OtelSpider.Core.BLL.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository hotelRepository;
        private readonly ISecurityUserRepository securityUserRepository;
        private readonly IHotelParentRepository hotelParentRepository;
        private readonly IUnitOfWork unitOfWork;
        public HotelService(IHotelRepository hotelRepository, IHotelParentRepository hotelParentRepository
            , ISecurityUserRepository securityUserRepository, IUnitOfWork unitOfWork)
        {
            this.hotelRepository = hotelRepository;
            this.hotelParentRepository = hotelParentRepository;
            this.securityUserRepository = securityUserRepository;
            this.unitOfWork = unitOfWork;
        }
        #region Hotels
        public void CreateHotel(Hotel item)
        {
            hotelRepository.Add(item);
        }
        public void UpdateHotel(Hotel item)
        {
            hotelRepository.Update(item);
        }
        public void DeleteHotel(int id)
        {
            var item = GetHotel(id);
            hotelRepository.Delete(item);
        }
        public Hotel GetHotel(int id)
        {
            return hotelRepository.Get(x => x.ID == id);
        }
        public Hotel GetHotelByName(string name)
        {
            return hotelRepository.Get(x => x.Name == name);
        }
        public IEnumerable<Hotel> GetHotels()
        {
            return hotelRepository.GetMany(r => true);
        }
        public IEnumerable<Hotel> GetHotelsByParentId(int parentId)
        {
            return hotelRepository.GetMany(h => h.ParentID == parentId);
        }
        public IEnumerable<Hotel> GetUserHotels(string systemUserID)
        {
            var userRoles = securityUserRepository.GetUserRoles(systemUserID);
            if(userRoles.Contains("SuperAdmin"))
            {
                return GetHotels();
            }
            var userHotels = hotelRepository.GetUserHotels(systemUserID).ToList();
            return userHotels.Select(x => x.Hotel);
        }

        public IEnumerable<Market> GetHotelMarkets(int hotelId)
        {
            var hotelMarkets = hotelRepository.GetHotelMarkets(hotelId).ToList();
            return hotelMarkets.Select(x => x.Market);
        }

        public IEnumerable<RoomType> GetHotelRoomTypes(int hotelId)
        {
            return hotelRepository.GetHotelRoomTypes(hotelId);
        }
        public IEnumerable<HotelBudget> GetHotelBudgets(int hotelId)
        {
            var hotelBudgets = hotelRepository.GetHotelBudgets(hotelId).ToList();
            return hotelBudgets;
        }
        public IEnumerable<HotelOTA> GetHotelOTAs(int hotelID)
        {
            return hotelRepository.GetHotelOTAs(hotelID);
        }
        public IEnumerable<HotelOTA> GetHotelOTAsByUser(string systemUserId)
        {
            var lstHotels = GetUserHotels(systemUserId).Select(h => h.ID).ToList();
            return hotelRepository.GetHotelsOTAs(lstHotels).Select(x => x);
        }
        #endregion Hotels

        #region Hotel Parent

        public void CreateHotelParent(HotelParent parentItem)
        {
            hotelParentRepository.Add(parentItem);
        }
        public void UpdateHotelParent(HotelParent parentItem)
        {
            hotelParentRepository.Update(parentItem);
        }
        public void DeleteHotelParent(int parentId)
        {
            var parentItem = GetHotelParent(parentId);
            hotelParentRepository.Delete(parentItem);
        }
        public HotelParent GetHotelParent(int parentId)
        {
            return hotelParentRepository.Get(x => x.ID == parentId);
        }
        public IEnumerable<HotelParent> GetHotelParents()
        {
            return hotelParentRepository.GetMany(x => true);
        }
        #endregion

        public void SaveHotel()
        {
            unitOfWork.Commit();
        }
    }
}
