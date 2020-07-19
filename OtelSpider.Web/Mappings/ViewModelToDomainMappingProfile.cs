using AutoMapper;
using OtelSpider.Core.DAL.Models;
using OtelSpider.Web.ViewModels;

namespace OtelSpider.Web.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<OTAViewModel, OTA>();
            CreateMap<HotelViewModel, Hotel>();
            CreateMap<HotelParentViewModel, HotelParent>();
            CreateMap<MonthlyReservationVM, MonthlyReservation>();
            CreateMap<DailyReservationVM, DailyReservation>();
            CreateMap<SingleReservationVM, SingleReservation>();
            CreateMap<UserViewModel, SystemUser>();
            CreateMap<BudgetViewModel, HotelBudget>();
            CreateMap<RoomNightsViewModel, AvailableRoomNights>();
            CreateMap<HotelMealPlanVM, HotelMealPlan>();
            CreateMap<CurrencyViewModel , Currency>();
            CreateMap<RoomTypeViewModel, RoomType>();
            CreateMap<HotelOTAViewModel, HotelOTA>();
            CreateMap<TokenViewModel, RegisterationToken>();
            CreateMap<RateStructureSettingVM, RateStructureSetting>();
            CreateMap<RSPeriodViewModel, RateStructurePeriod>();
            CreateMap<RoomMealPlanVM, RoomMealPlan>();
            CreateMap<occupancyFormulaVM, OccupancyFormula>();
        }
    }
}