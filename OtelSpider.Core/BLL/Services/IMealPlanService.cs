using OtelSpider.Core.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.BLL.Services
{
    public interface IMealPlanService
    {
        #region Meal Plans

        void Create(MealPlan item);
        void Update(MealPlan item);
        void Delete(int id);
        MealPlan GetMealPlan(int id);
        MealPlan GetMealPlanByName(string name);
        IEnumerable<MealPlan> GetMealPlans();

        #endregion

        #region Hotel Meal Plans
        void AddHotelMealPlan(HotelMealPlan item);
        void UpdateHotelMealPlan(HotelMealPlan item);
        void DeleteHotelMealPlan(int id);
        HotelMealPlan GetHotelMealPlan(int id);
        IEnumerable<HotelMealPlan> GetHotelMealPlansByHotelId(int hotelId);
        IEnumerable<HotelMealPlan> GetMealPlansForHotels(List<int> hotelIds);
        void updateBaseMeal(int hotelId, int baseMealID = 0); 
        #endregion

        void SaveChanges();
    }
}
