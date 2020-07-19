using OtelSpider.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OtelSpider.Web.Helpers
{
    public static class RateStructureHelper
    {
        public static decimal CalculateRate(RateStructureReportVM Model, int rsPeriodId, int roomId, int formulaId, int mealplanId)
        {
            var rate = Model.RSPeriods.FirstOrDefault(p => p.ID == rsPeriodId).RoomSupplement;

            var room = Model.RoomTypes.FirstOrDefault(r => r.ID == roomId);
            var mealplan = Model.hotelMealPlans.FirstOrDefault(m => m.MealPlanID == mealplanId);
            var formula = room.OccupancyFormulas.FirstOrDefault(f => f.ID == formulaId);

            if (!room.IsBase)
            {
                if (room.isSubstraction)
                {
                    if (room.isPrecentage)
                    {
                        rate = rate - ((room.AdditionValue / rate) * 100);
                    }
                    else
                    {
                        rate = rate - room.AdditionValue;
                    }
                }
                else
                {
                    if (room.isPrecentage)
                    {
                        rate = rate + ((room.AdditionValue / rate) * 100);
                    }
                    else
                    {
                        rate = rate + room.AdditionValue;
                    }
                }
            }
            // formaula calculations
            if (!formula.IsBase)
            {
                if (formula.isSubstraction)
                {
                    if (formula.isPrecentage)
                    {
                        rate = rate - ((formula.AdditionalValue / rate) * 100);
                    }
                    else
                    {
                        rate = rate - formula.AdditionalValue;
                    }
                }
                else
                {
                    if (formula.isPrecentage)
                    {
                        rate = rate + ((formula.AdditionalValue / rate) * 100);
                    }
                    else
                    {
                        rate = rate + formula.AdditionalValue;
                    }
                }
            }
            //mealplan calculations
            if (!mealplan.IsBase)
            {
                if (mealplan.isSubstraction)
                {
                    if (mealplan.isPrecentage)
                    {
                        rate = rate - ((mealplan.MealPlanSupplement / rate) * 100);
                    }
                    else
                    {
                        rate = rate - mealplan.MealPlanSupplement;
                    }
                }
                else
                {
                    if (mealplan.isPrecentage)
                    {
                        rate = rate + ((mealplan.MealPlanSupplement / rate) * 100);
                    }
                    else
                    {
                        rate = rate + mealplan.MealPlanSupplement;
                    }
                }
            }
            else
            {
                rate = rate + Model.RSPeriods.FirstOrDefault(p => p.ID == rsPeriodId).MealSupplement;
            }

            return rate;
        }
    }
}