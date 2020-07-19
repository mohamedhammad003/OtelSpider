using AutoMapper;
using OtelSpider.Core.BLL.Services;
using OtelSpider.Core.DAL.Models;
using OtelSpider.Web.Helpers;
using OtelSpider.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OtelSpider.Web.Controllers
{
    public class BudgetController : Controller
    {
        private readonly IBudgetService _budgetService;
        private readonly IRoomNightService _roomNightService;
        private readonly IHotelService _hotelService;
        private readonly ICurrencyService _currencyService;
        private readonly string CurrentUserId;

        public BudgetController(IBudgetService budgetService
            , IHotelService hotelService
            , ICurrencyService currencyService
            ,IRoomNightService roomNightService)
        {
            _budgetService = budgetService;
            _roomNightService = roomNightService;
            _hotelService = hotelService;
            _currencyService = currencyService;
            CurrentUserId = UserHelper.Current.UserInfo.UserId;
        }
        public ActionResult Index(int? year = null)
        {
            var lstHotelModel = _hotelService.GetUserHotels(CurrentUserId);
            ViewBag.Hotels = new SelectList(lstHotelModel.Select(x =>
             new SelectListItem() { Text = x.Name.ToString(), Value = x.ID.ToString() }), "Value", "Text");

            var hotelId = lstHotelModel.Count() > 0 ? lstHotelModel.First().ID : 0;
            year = year.HasValue ? year : DateTime.Now.Year;
            var lstBudgetModel = _budgetService.GetHotelBudgets(hotelId);
            var lstRoomNightsModel = _roomNightService.GetHotelRoomNights(hotelId);
            var lstBudgetVM = Mapper.Map<IEnumerable<HotelBudget>, IEnumerable<BudgetViewModel>>(lstBudgetModel);
            var lstRoomsVM = Mapper.Map<IEnumerable<AvailableRoomNights>, IEnumerable<RoomNightsViewModel>>(_roomNightService.GetHotelRoomNights(hotelId));

            List<HotelBudgetViewModel> lstHotelBugdetVM = new List<HotelBudgetViewModel>();

            foreach(var item in lstBudgetVM)
            {
                var hotelBudgetVM = new HotelBudgetViewModel();
                hotelBudgetVM.Budget = item;
                hotelBudgetVM.RoomNights = lstRoomsVM.FirstOrDefault(r => r.Year == item.Year)  != null ? lstRoomsVM.FirstOrDefault(r => r.Year == item.Year) : new RoomNightsViewModel();
                lstHotelBugdetVM.Add(hotelBudgetVM);
            }
            return View(lstHotelBugdetVM);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Months = new SelectList(Enumerable.Range(1, 12).Select(x =>
              new SelectListItem()
              {
                  Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1] + " (" + x + ")"
                 ,
                  Value = x.ToString()
              }), "Value", "Text");

            ViewBag.Years = new SelectList(Enumerable.Range(DateTime.Today.Year - 5, 10).Select(x =>
              new SelectListItem() { Text = x.ToString(), Value = x.ToString() }), "Value", "Text");

            var lstHotelModel = _hotelService.GetUserHotels(CurrentUserId);
            ViewBag.Hotels = new SelectList(lstHotelModel.Select(x =>
             new SelectListItem() { Text = x.Name.ToString(), Value = x.ID.ToString() }), "Value", "Text");

            var lstCurrencies = _currencyService.GetCurrencies();
            ViewBag.Currencies = new SelectList(lstCurrencies.Select(x =>
             new SelectListItem() { Text = x.CurrencyCode.ToString(), Value = x.ID.ToString() }), "Value", "Text");

            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var monthlyBudget = _budgetService.GetBudget(id);
            var budgetVM = Mapper.Map<HotelBudget, BudgetViewModel>(monthlyBudget);

            var hotelRoomNights = _roomNightService.GetYearAvailableRoomNight(monthlyBudget.HotelID,monthlyBudget.Year);
            var roomNightVM = Mapper.Map<AvailableRoomNights, RoomNightsViewModel>(hotelRoomNights);

            ViewBag.Years = new SelectList(Enumerable.Range(DateTime.Today.Year - 5, 10).Select(x =>
              new SelectListItem() { Text = x.ToString(), Value = x.ToString(), Selected = x == budgetVM.Year }), "Value", "Text");

            var lstHotelModel = _hotelService.GetUserHotels(CurrentUserId);
            ViewBag.Hotels = new SelectList(lstHotelModel.Select(x =>
             new SelectListItem() { Text = x.Name.ToString(), Value = x.ID.ToString(), Selected = x.ID == budgetVM.HotelID }), "Value", "Text");

            var lstCurrencies = _currencyService.GetCurrencies();
            ViewBag.Currencies = new SelectList(lstCurrencies.Select(x =>
             new SelectListItem() { Text = x.CurrencyCode.ToString(), Value = x.ID.ToString(), Selected = x.ID == budgetVM.CurrencyID }), "Value", "Text");

            var hotelBudgetVM = new HotelBudgetViewModel() { Budget = budgetVM , RoomNights = roomNightVM};
            return View(hotelBudgetVM);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var monthlyBudget = _budgetService.GetBudget(id);
            var hotelRoomNights = _roomNightService.GetYearAvailableRoomNight(monthlyBudget.HotelID, monthlyBudget.Year);

            _budgetService.Delete(id);
            _budgetService.SaveChanges();

            _roomNightService.Delete(hotelRoomNights.ID);
            _roomNightService.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Create(HotelBudgetViewModel hotelbudgetVM)
        {
            hotelbudgetVM.RoomNights.HotelID = hotelbudgetVM.Budget.HotelID;
            hotelbudgetVM.RoomNights.Year = hotelbudgetVM.Budget.Year;

            var budgetModel = Mapper.Map<BudgetViewModel, HotelBudget> (hotelbudgetVM.Budget);
            var roomNightsModel = Mapper.Map<RoomNightsViewModel, AvailableRoomNights>(hotelbudgetVM.RoomNights);

            _budgetService.Create(budgetModel);
            _budgetService.SaveChanges();

            _roomNightService.Create(roomNightsModel);
            _roomNightService.SaveChanges();
            
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(HotelBudgetViewModel hotelbudgetVM)
        {
            hotelbudgetVM.RoomNights.HotelID = hotelbudgetVM.Budget.HotelID;
            hotelbudgetVM.RoomNights.Year = hotelbudgetVM.Budget.Year;

            var budgetModel = Mapper.Map<BudgetViewModel, HotelBudget>(hotelbudgetVM.Budget);
            var roomNightsModel = Mapper.Map<RoomNightsViewModel, AvailableRoomNights>(hotelbudgetVM.RoomNights);
            
            _budgetService.Update(budgetModel);
            _budgetService.SaveChanges();

            _roomNightService.Update(roomNightsModel);
            _roomNightService.SaveChanges();
            
            return RedirectToAction("Index");
        }
    }
}