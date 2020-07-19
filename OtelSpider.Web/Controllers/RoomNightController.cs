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
    public class RoomNightController : Controller
    {
        private readonly IRoomNightService _roomNightService;
        private readonly IHotelService _hotelService;
        private readonly ICurrencyService _currencyService;
        private readonly string CurrentUserId;

        public RoomNightController(IRoomNightService roomNightService, IHotelService hotelService, ICurrencyService currencyService)
        {
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
            var lstRoomsVM = Mapper.Map<IEnumerable<AvailableRoomNights>, IEnumerable<RoomNightsViewModel>>(_roomNightService.GetHotelRoomNights(hotelId));
            return View(lstRoomsVM);
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
            var hotelRoomNights = _roomNightService.GetAvailableRoomNight(id);
            var roomNightVM = Mapper.Map<AvailableRoomNights, RoomNightsViewModel>(hotelRoomNights);
            var lstHotelModel = _hotelService.GetUserHotels(CurrentUserId);

            ViewBag.Years = new SelectList(Enumerable.Range(DateTime.Today.Year - 5, 10).Select(x =>
              new SelectListItem() { Text = x.ToString(), Value = x.ToString(), Selected = x == roomNightVM.Year }), "Value", "Text");

            ViewBag.Hotels = new SelectList(lstHotelModel.Select(x =>
             new SelectListItem() { Text = x.Name.ToString(), Value = x.ID.ToString(), Selected = x.ID == roomNightVM.HotelID }), "Value", "Text");

            
            return View(roomNightVM);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            _roomNightService.Delete(id);
            _roomNightService.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Create(RoomNightsViewModel roomNightVM)
        {
            var RoomNightModel = Mapper.Map<RoomNightsViewModel, AvailableRoomNights>(roomNightVM);
            _roomNightService.Create(RoomNightModel);
            _roomNightService.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(RoomNightsViewModel roomNightVM)
        {
            var RoomNightModel = Mapper.Map<RoomNightsViewModel, AvailableRoomNights>(roomNightVM);
            _roomNightService.Update(RoomNightModel);
            _roomNightService.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}