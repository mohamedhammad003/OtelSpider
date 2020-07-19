using OtelSpider.Core.BLL.Repositories;
using OtelSpider.Core.DAL.Models;
using OtelSpider.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.BLL.Services
{
    public class OccupancyFormulaService : IOccupancyFormulaService
    {

        private readonly IOccupancyFormulaRepository occupancyFormulaRepository;
        private readonly IUnitOfWork unitOfWork;
        public OccupancyFormulaService(IOccupancyFormulaRepository occupancyFormulaRepository
            , IRoomMealPlanRepository roomMealPlanRepository
            , IUnitOfWork unitOfWork)
        {
            this.occupancyFormulaRepository = occupancyFormulaRepository;
            this.unitOfWork = unitOfWork;
        }
        public void Create(OccupancyFormula item)
        {
            occupancyFormulaRepository.Add(item);
        }
        public void Update(OccupancyFormula item)
        {
            occupancyFormulaRepository.Update(item);
        }
        public void Delete(int id)
        {
            var item = GetOccupancyFormula(id);
            occupancyFormulaRepository.Delete(item);
        }
        public void DeleteRoomFormulas(int roomId)
        {
            var formulas = GetOccupancyFormulasByRoom(roomId);
            foreach (var item in formulas)
            {
                occupancyFormulaRepository.Delete(item);
            }
        }
        public OccupancyFormula GetOccupancyFormula(int id)
        {
            return occupancyFormulaRepository.Get(f => f.ID == id);
        }
        public OccupancyFormula GetOccupancyFormula(int roomId, int capacity)
        {
            return occupancyFormulaRepository.Get(f => f.RoomTypeID == roomId && f.Capacity == capacity);
        }
        public IEnumerable<OccupancyFormula> GetOccupancyFormulasByRoom(int roomId)
        {
            return occupancyFormulaRepository.GetMany(f => f.RoomTypeID == roomId);
        }
        public void SaveChanges()
        {
            unitOfWork.Commit();
        }
    }
}
