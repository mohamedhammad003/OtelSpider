using OtelSpider.Core.DAL.Models;
using OtelSpider.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.BLL.Repositories
{
    public class RoomTypeRepository : RepositoryBase<RoomType>, IRoomTypeRepository
    {
        public RoomTypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        public IEnumerable<RoomType> GetRoomTypesByUserID(string systemUserID)
        {
            var userHotelIDs = DbContext.UserHotels.Where(h => h.UserID == systemUserID).Select(h => h.HotelID);
            return DbContext.RoomTypes.Where(r => userHotelIDs.Contains(r.HotelID));
        }
    }
    public interface IRoomTypeRepository: IRepository<RoomType>
    {
        IEnumerable<RoomType> GetRoomTypesByUserID(string systemUserID);
    }
}
