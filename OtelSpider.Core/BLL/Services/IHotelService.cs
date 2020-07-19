
using OtelSpider.Core.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.BLL.Services
{
    public interface IHotelService
    {
        #region Hotels
        void CreateHotel(Hotel item);
        void UpdateHotel(Hotel item);
        void DeleteHotel(int id);
        Hotel GetHotel(int id);
        Hotel GetHotelByName(string name);
        IEnumerable<Hotel> GetHotels();
        IEnumerable<Hotel> GetHotelsByParentId(int parentId);
        IEnumerable<Hotel> GetUserHotels(string systemUserID);
        IEnumerable<Market> GetHotelMarkets(int hotelId);
        IEnumerable<RoomType> GetHotelRoomTypes(int hotelId);
        IEnumerable<HotelBudget> GetHotelBudgets(int hotelId);
        IEnumerable<HotelOTA> GetHotelOTAs(int hotelID);
        IEnumerable<HotelOTA> GetHotelOTAsByUser(string systemUserId);
        #endregion

        #region Hotel Parent
        void CreateHotelParent(HotelParent parentItem);
        void UpdateHotelParent(HotelParent parentItem);
        void DeleteHotelParent(int parentId);
        HotelParent GetHotelParent(int parentId);
        IEnumerable<HotelParent> GetHotelParents();
        #endregion

        void SaveHotel();
    }
}
