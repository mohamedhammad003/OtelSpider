using AutoMapper;
using OtelSpider.Core.DAL.Models;
using OtelSpider.Web.ViewModels;

namespace OtelSpider.Web.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        public DomainToViewModelMappingProfile()
        {
            CreateMap<OTA, OTAViewModel>();
            CreateMap<MonthlyReservation, MonthlyReservationVM>();
            CreateMap<DailyReservation, DailyReservationVM>();
            CreateMap<SingleReservation, SingleReservationVM>()
                .ForMember(x => x.Status, map => map.MapFrom(s => s.Status.Status))
                .ForMember(x => x.MealPlanName, map => map.MapFrom(s => s.MealPlan.Abbreviation))
                .ForMember(x => x.CurrencyCode, map => map.MapFrom(s => s.Currency.CurrencyCode));
            CreateMap<SystemUser, UserViewModel>();
            CreateMap<Hotel, HotelViewModel>();
            CreateMap<HotelParent, HotelParentViewModel>();
            CreateMap<HotelBudget, BudgetViewModel>();
            CreateMap<AvailableRoomNights, RoomNightsViewModel>();
            CreateMap<Currency, CurrencyViewModel>();
            CreateMap<RoomType, RoomTypeViewModel>();
            CreateMap<HotelOTA, HotelOTAViewModel>()
                .ForMember(x => x.HotelName, map => map.MapFrom(s => s.Hotel.Name))
                .ForMember(x => x.OTAName, map => map.MapFrom(s => s.OTA.Name));

            CreateMap<HotelMealPlan, HotelMealPlanVM>()
                .ForMember(x => x.MealPlanName, map => map.MapFrom(s => s.MealPlan.Name))
                .ForMember(x => x.Abbreviation, map => map.MapFrom(s => s.MealPlan.Abbreviation));
            CreateMap<RegisterationToken, TokenViewModel>();
            CreateMap<RateStructureSetting, RateStructureSettingVM>();
            CreateMap<RateStructurePeriod, RSPeriodViewModel>();
            CreateMap<OccupancyFormula, occupancyFormulaVM>();
            CreateMap<RoomMealPlan, RoomMealPlanVM>()
                .ForMember(x => x.MealPlanName, map => map.MapFrom(s => s.MealPlan.Name));
        }
    }
}
