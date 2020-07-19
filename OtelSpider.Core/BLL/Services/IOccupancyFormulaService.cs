using OtelSpider.Core.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.BLL.Services
{
    public interface IOccupancyFormulaService
    {
        void Create(OccupancyFormula item);
        void Update(OccupancyFormula item);
        void Delete(int id);
        void DeleteRoomFormulas(int roomId);
        OccupancyFormula GetOccupancyFormula(int id);
        OccupancyFormula GetOccupancyFormula(int roomId, int capacity);
        IEnumerable<OccupancyFormula> GetOccupancyFormulasByRoom(int roomId);
        void SaveChanges();
    }
}
