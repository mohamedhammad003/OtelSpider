using AutoMapper;
using OtelSpider.Core.BLL.Services;
using OtelSpider.Core.DAL.Models;
using OtelSpider.Web.ActionFilter;
using OtelSpider.Web.Helpers;
using OtelSpider.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OtelSpider.Web.Controllers
{
    public class RateStructureController : Controller
    {
        private readonly IRateStructureSettingService _rateStructureSettingService;
        private readonly IRateStructurePeriodService _rateStructurePeriodService;
        private readonly IRoomTypeService _roomTypeService;
        private readonly IHotelService _hotelService;
        private readonly ICurrencyService _currencyService;
        private readonly IMealPlanService _mealPlanService;

        private readonly string CurrentUserId;
        public RateStructureController(IRateStructureSettingService rateStructureSettingService
            , IRateStructurePeriodService rateStructurePeriodService
            , IHotelService hotelService
            , IRoomTypeService roomTypeService
            , ICurrencyService currencyService
            , IMealPlanService mealPlanService)
        {
            _rateStructureSettingService = rateStructureSettingService;
            _rateStructurePeriodService = rateStructurePeriodService;
            _hotelService = hotelService;
            _roomTypeService = roomTypeService;
            _currencyService = currencyService;
            _mealPlanService = mealPlanService;
            CurrentUserId = UserHelper.Current.UserInfo.UserId;
        }
        
        #region RS Settings
        public ActionResult RateStructureSettings(int hotelId)
        {
            var setting = _rateStructureSettingService.GetHotelRateStructureSetting(hotelId);
            var lstRoomTypesModel = _roomTypeService.GetRoomTypesByHotelID(hotelId);
            var lstCurrencies = _currencyService.GetCurrencies();
            var lstHotelModel = _hotelService.GetUserHotels(CurrentUserId);

            if (setting != null)
            {
                ViewBag.RoomTypes = new SelectList(lstRoomTypesModel.Select(x =>
                new SelectListItem() { Text = x.Type.ToString(), Value = x.ID.ToString(), Selected = x.ID == setting.BaseRoomTypeID }), "Value", "Text");

                ViewBag.baseCurrencies = new SelectList(lstCurrencies.Select(x =>
                new SelectListItem() { Text = x.CurrencyCode.ToString(), Value = x.ID.ToString(), Selected = x.ID == setting.BaseCurrencyID }), "Value", "Text");

                ViewBag.shownCurrencies = new SelectList(lstCurrencies.Select(x =>
                new SelectListItem() { Text = x.CurrencyCode.ToString(), Value = x.ID.ToString(), Selected = x.ID == setting.ShownCurrencyID }), "Value", "Text");

                ViewBag.Hotels = new SelectList(lstHotelModel.Select(x =>
             new SelectListItem() { Text = x.Name.ToString(), Value = x.ID.ToString(), Selected = x.ID == setting.HotelID }), "Value", "Text");

                var settingVM = Mapper.Map<RateStructureSetting, RateStructureSettingVM>(setting);
                return View("EditRateStructureSettings", settingVM);
            }
            ViewBag.RoomTypes = new SelectList(lstRoomTypesModel.Select(x =>
                new SelectListItem() { Text = x.Type.ToString(), Value = x.ID.ToString() }), "Value", "Text");

            ViewBag.Currencies = new SelectList(lstCurrencies.Select(x =>
            new SelectListItem() { Text = x.CurrencyCode.ToString(), Value = x.ID.ToString() }), "Value", "Text");

            ViewBag.Hotels = new SelectList(lstHotelModel.Select(x =>
             new SelectListItem() { Text = x.Name.ToString(), Value = x.ID.ToString() }), "Value", "Text");

            return View("RateStructureSettings");
        }
        [HttpPost]
        public ActionResult RateStructureSettings(RateStructureSettingVM settingVM)
        {
            var setting = Mapper.Map<RateStructureSettingVM, RateStructureSetting>(settingVM);
            _rateStructureSettingService.Create(setting);
            _rateStructureSettingService.SaveChanges();
            return RedirectToAction("RateStructureSettings", new { hotelId = settingVM.HotelID });
        }
        [HttpPost]
        public ActionResult EditRateStructureSettings(RateStructureSettingVM settingVM)
        {
            var setting = Mapper.Map<RateStructureSettingVM, RateStructureSetting>(settingVM);
            _rateStructureSettingService.Update(setting);
            _rateStructureSettingService.SaveChanges();
            return RedirectToAction("RateStructureSettings", new { hotelId = settingVM.HotelID });
        }
        #endregion

        #region RS Periods
        public ActionResult RateStructurePeriod(int hotelId)
        {
            var currentYear = DateTime.Now.Year;
            var PeriodVm = Mapper.Map<IEnumerable<RateStructurePeriod>, IEnumerable<RSPeriodViewModel>>(_rateStructurePeriodService.GetRateStructurePeriodsByYear(hotelId, currentYear));
            return View(PeriodVm);
        }
        public ActionResult EditRSPeriod(int Id)
        {
            var PeriodVm = Mapper.Map<RateStructurePeriod, RSPeriodViewModel>(_rateStructurePeriodService.GetRateStructurePeriod(Id));
            var lstHotelModel = _hotelService.GetUserHotels(CurrentUserId);
            ViewBag.Hotels = new SelectList(lstHotelModel.Select(x =>
             new SelectListItem() { Text = x.Name.ToString(), Value = x.ID.ToString(), Selected = x.ID == PeriodVm.HotelID }), "Value", "Text");

            return View(PeriodVm);
        }
        [HttpPost]
        public ActionResult EditRSPeriod(RSPeriodViewModel RSPeriodVM)
        {
            var PeriodModel = Mapper.Map<RSPeriodViewModel, RateStructurePeriod>(RSPeriodVM);
            _rateStructurePeriodService.Update(PeriodModel);
            _rateStructurePeriodService.SaveChanges();
            return RedirectToAction("RateStructurePeriod", RSPeriodVM.HotelID);
        }
        public ActionResult AddRSPeriod()
        {
            var lstHotelModel = _hotelService.GetUserHotels(CurrentUserId);
            ViewBag.Hotels = new SelectList(lstHotelModel.Select(x =>
             new SelectListItem() { Text = x.Name.ToString(), Value = x.ID.ToString() }), "Value", "Text");
            return View();
        }
        [HttpPost]
        public ActionResult AddRSPeriod(RSPeriodViewModel RSPeriodVM)
        {
            var PeriodModel = Mapper.Map<RSPeriodViewModel, RateStructurePeriod>(RSPeriodVM);
            _rateStructurePeriodService.Create(PeriodModel);
            _rateStructurePeriodService.SaveChanges();
            return RedirectToAction("RateStructurePeriod", RSPeriodVM.HotelID);
        }
        public ActionResult DeleteRSPeriod(int Id, int hotelID)
        {
            _rateStructurePeriodService.Delete(Id);
            _rateStructurePeriodService.SaveChanges();
            return RedirectToAction("RateStructurePeriod", hotelID);

        }
        #endregion

        #region RS Report Views
        [AuthorizeRole(PermissionName = "SuperAdmin")]
        public ActionResult Index(int hotelId, int year)
        {
            var setting = _rateStructureSettingService.GetHotelRateStructureSetting(hotelId);
            var lstMealPlansModel = _mealPlanService.GetHotelMealPlansByHotelId(hotelId);
            var lstMealPlansVM = Mapper.Map<IEnumerable<HotelMealPlan>, IEnumerable<HotelMealPlanVM>>(lstMealPlansModel);
            var lstRoomsVM = Mapper.Map<IEnumerable<RoomType>, IEnumerable<RoomTypeViewModel>>(_roomTypeService.GetRoomTypesByHotelID(hotelId));
            var lstPeriodsVm = Mapper.Map<IEnumerable<RateStructurePeriod>, IEnumerable<RSPeriodViewModel>>(_rateStructurePeriodService.GetRateStructurePeriodsByYear(hotelId, year));
            var rateStructureSet = Mapper.Map<RateStructureSetting, RateStructureSettingVM>(setting);

            var rateStructureReportVM = new RateStructureReportVM {
                HotelID = hotelId,
                Year = year,
                rateStructureSettings = rateStructureSet,
                RoomTypes = lstRoomsVM.ToList(),
                RSPeriods = lstPeriodsVm.ToList(),
                hotelMealPlans = lstMealPlansVM.ToList()
            };
            return View(rateStructureReportVM);
        }
        #endregion
    }
}