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
    public class MealPlanService : IMealPlanService
    {
        private readonly IMealPlanRepository mealPlanRepository;
        private readonly IHotelMealPlanRepository hotelMealPlanRepository;
        private readonly IUnitOfWork unitOfWork;
        public MealPlanService(IMealPlanRepository mealPlanRepository
            , IHotelMealPlanRepository hotelMealPlanRepository
            , IUnitOfWork unitOfWork)
        {
            this.mealPlanRepository = mealPlanRepository;
            this.hotelMealPlanRepository = hotelMealPlanRepository;
            this.unitOfWork = unitOfWork;
        }
        #region Meal Plan
        public void Create(MealPlan item)
        {
            mealPlanRepository.Add(item);
        }
        public void Update(MealPlan item)
        {
            mealPlanRepository.Update(item);
        }
        public void Delete(int id)
        {
            var item = GetMealPlan(id);
            mealPlanRepository.Delete(item);
        }
        public MealPlan GetMealPlan(int id)
        {
            return mealPlanRepository.Get(m => m.ID == id);
        }
        public MealPlan GetMealPlanByName(string name)
        {
            return mealPlanRepository.Get(m => m.Name == name);
        }
        public IEnumerable<MealPlan> GetMealPlans()
        {
            return mealPlanRepository.GetMany(r => true);
        }
        #endregion
        #region Hotel Meal Plans 
        public void AddHotelMealPlan(HotelMealPlan item)
        {
            if (item.IsBase)
            {
                var hotelMeals = GetHotelMealPlansByHotelId(item.HotelID);
                foreach (var meal in hotelMeals)
                {
                    meal.IsBase = false;
                    hotelMealPlanRepository.Update(meal);
                }
            }

            hotelMealPlanRepository.Add(item);
        }
        public void UpdateHotelMealPlan(HotelMealPlan item)
        {
            if (item.IsBase)
            {
                var hotelMeals = GetHotelMealPlansByHotelId(item.HotelID);
                foreach (var meal in hotelMeals)
                {
                    if (meal.ID != item.ID)
                    {
                        meal.IsBase = false;
                        hotelMealPlanRepository.Update(meal);
                    }
                }
            }
            hotelMealPlanRepository.Update(item);
        }
        public void DeleteHotelMealPlan(int id)
        {
            var item = GetHotelMealPlan(id);
            hotelMealPlanRepository.Delete(item);
        }
        public HotelMealPlan GetHotelMealPlan(int id)
        {
            return hotelMealPlanRepository.Get(m => m.ID == id);
        }
        public IEnumerable<HotelMealPlan> GetHotelMealPlansByHotelId(int hotelId)
        {
            return hotelMealPlanRepository.GetMany(m => m.HotelID == hotelId);
        }
        public IEnumerable<HotelMealPlan> GetMealPlansForHotels(List<int> hotelIds)
        {
            return hotelMealPlanRepository.GetMany(r => hotelIds.Contains(r.HotelID));
        }
        public void updateBaseMeal(int hotelId, int baseMealID = 0)
        {
            var hotelMeals = GetHotelMealPlansByHotelId(hotelId);
            foreach (var meal in hotelMeals)
            {
                if (meal.ID != baseMealID)
                {
                    meal.IsBase = false;
                    hotelMealPlanRepository.Update(meal);
                }
            }
        }
        #endregion
        public void SaveChanges()
        {
            unitOfWork.Commit();
        }
    }
}
