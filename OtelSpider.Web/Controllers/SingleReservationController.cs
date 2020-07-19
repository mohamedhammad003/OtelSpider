using AutoMapper;
using OtelSpider.Core.BLL.Services;
using OtelSpider.Core.DAL.Models;
using OtelSpider.Web.ActionFilter;
using OtelSpider.Web.Helpers;
using OtelSpider.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OtelSpider.Web.Controllers
{
    public class SingleReservationController : Controller
    {
        private readonly ISingleReservationService _singleReservationService;
        private readonly IOTAService _OTAService;
        private readonly IHotelService _hotelService;
        private readonly IReservationStatusService _reservationStatusService;
        private readonly IMealPlanService _mealPlanService;
        private readonly ICurrencyService _currencyService;

        public SingleReservationController(ISingleReservationService singleReservationService
            , IOTAService OTAService
            , IHotelService hotelService
            , IReservationStatusService reservationStatusService
            , IMealPlanService mealPlanService
            , ICurrencyService currencyService)
        {
            _singleReservationService = singleReservationService;
            _OTAService = OTAService;
            _hotelService = hotelService;
            _reservationStatusService = reservationStatusService;
            _mealPlanService = mealPlanService;
            _currencyService = currencyService;
        }
        [AuthorizeRole(PermissionName = "SuperAdmin")]
        public ActionResult Index(int? hotelId = null, int? otaId = null, int? year = null, int? month = null)
        {
            var viewModel = new SingleReservationViewModel();

            //if (!year.HasValue)
            //    year = viewModel.SelectedYear;
            //if (!month.HasValue)
            //    month = viewModel.SelectedMonth;
            viewModel.singleReservations = Mapper.Map<IEnumerable<SingleReservation>, IEnumerable<SingleReservationVM>>(_singleReservationService.GetFilteredSingleReservation(hotelId, otaId, year, month));
            viewModel.lstHotels = Mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelViewModel>>(_hotelService.GetUserHotels(UserHelper.Current.UserInfo.UserId)); ;
            viewModel.lstOTAs = Mapper.Map<IEnumerable<OTA>, IEnumerable<OTAViewModel>>(_OTAService.GetOTAs());

            ViewBag.Months = new SelectList(Enumerable.Range(1, 12).Select(x =>
              new SelectListItem()
              {
                  Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1] + " (" + x + ")"
                 ,
                  Value = x.ToString()
              }), "Value", "Text");

            ViewBag.Years = new SelectList(Enumerable.Range(DateTime.Today.Year - 5, 10).Select(x =>
              new SelectListItem() { Text = x.ToString(), Value = x.ToString() }), "Value", "Text");

            return View(viewModel);
        }
        [HttpGet]
        public ActionResult FilteredSingleReservations(int? hotelId = null, int? otaId = null, int? year = null, int? month = null)
        {
            var lstReservationModel = _singleReservationService.GetFilteredSingleReservation(hotelId, otaId, year, month);
            IEnumerable<SingleReservationVM> lstReservations = Mapper.Map<IEnumerable<SingleReservation>, IEnumerable<SingleReservationVM>>(lstReservationModel);
            return Json(lstReservations, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult _GetSingleResList(IEnumerable<SingleReservationVM> singleReservations)
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult uploadSingleReport(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads/ReservationDetails"), fileName);
                file.SaveAs(path);

                var jsonReport = ExcelToJson.ConvertExcelToJson(path);
                List<string> uploadErrors = new List<string>();
                foreach (var item in jsonReport)
                {
                    var otaData = _OTAService.GetOTAByName(item["OTA"].ToString());
                    var hotelData = _hotelService.GetHotelByName(item["Hotel"].ToString());
                    var roomTypeData = _hotelService.GetHotelRoomTypes(hotelData.ID).FirstOrDefault(x => x.Type == item["Room type"].ToString());
                    var statusData = _reservationStatusService.GetReservationStatusByName(item["Status"].ToString());
                    var mealPlanData = _mealPlanService.GetMealPlanByName(item["Meal Plan"].ToString());
                    var currencyData = _currencyService.GetCurrencyByName(item["Currency"].ToString());

                    var ReservationRefID = item["Reservation Ref ID"].ToString();
                    int roomNights = Convert.ToInt32(item["Room Nights"]);

                    if (otaData == null)
                    {
                        var error = "there is no OTA with the Name : " + item["OTA"].ToString();
                        if (!uploadErrors.Contains(error))
                            uploadErrors.Add(error);
                        continue;
                    }
                    if (hotelData == null)
                    {
                        var error = "there is no Hotel with the Name : " + item["Hotel"].ToString();
                        if (!uploadErrors.Contains(error))
                            uploadErrors.Add(error);
                        continue;
                    }
                    if (roomTypeData == null)
                    {
                        var error = "there is no Room type with the Name : " + item["Room type"].ToString();
                        if (!uploadErrors.Contains(error))
                            uploadErrors.Add(error);
                        //continue;
                    }
                    if (statusData == null)
                    {
                        var error = "there is no Reservation Status with the Name : " + item["Status"].ToString();
                        if (!uploadErrors.Contains(error))
                            uploadErrors.Add(error);
                        continue;
                    }
                    if (mealPlanData == null)
                    {
                        var error = "there is no Meal Plan with the Name : " + item["Meal Plan"].ToString();
                        if (!uploadErrors.Contains(error))
                            uploadErrors.Add(error);
                        continue;
                    }
                    if (currencyData == null)
                    {
                        var error = "there is no Currency with the Name : " + item["Currency"].ToString();
                        if (!uploadErrors.Contains(error))
                            uploadErrors.Add(error);
                        continue;
                    }

                    var singleReservation = _singleReservationService.GetSingleReservationByRefID(ReservationRefID);

                    if (singleReservation != null)
                    {
                        singleReservation.OTAID = otaData.ID;
                        singleReservation.HotelID = hotelData.ID;
                        singleReservation.VisitorName = item["Visitor Name"].ToString();
                        singleReservation.VisitorEmail = item["Visitor Email"].ToString();
                        singleReservation.PhoneNum = item["Phone"].ToString();
                        singleReservation.Nationality = item["Nationality"].ToString();
                        singleReservation.RoomTypeID = roomTypeData != null ? roomTypeData.ID : 1;
                        singleReservation.StatusID = statusData.ID;
                        singleReservation.NightsNum = roomNights;
                        singleReservation.CheckinDate = Convert.ToDateTime(item["Checkin"]);
                        singleReservation.CheckoutDate = Convert.ToDateTime(item["Checkout"]);
                        singleReservation.MealPlanID = mealPlanData.ID;
                        singleReservation.RoomPrice = Convert.ToDecimal(item["Room Price"]);
                        singleReservation.Price = Convert.ToDecimal(item["Price"]);
                        singleReservation.Discount = Convert.ToDecimal(item["Discount"]);
                        singleReservation.Total = Convert.ToDecimal(item["Total"]);
                        singleReservation.CurrencyID = currencyData.ID;
                        singleReservation.PromoCode = item["Promo Code"].ToString();

                        _singleReservationService.Update(singleReservation);
                    }
                    else
                    {
                        var reservation = new Core.DAL.Models.SingleReservation()
                        {
                            ReservationRefID = ReservationRefID,
                            OTAID = otaData.ID,
                            HotelID = hotelData.ID,
                            VisitorName = item["Visitor Name"].ToString(),
                            VisitorEmail = item["Visitor Email"].ToString(),
                            PhoneNum = item["Phone"].ToString(),
                            Nationality = item["Nationality"].ToString(),
                            RoomTypeID = roomTypeData != null ? roomTypeData.ID : 1,
                            StatusID = statusData.ID,
                            NightsNum = roomNights,
                            CheckinDate = Convert.ToDateTime(item["Checkin"]),
                            CheckoutDate = Convert.ToDateTime(item["Checkout"]),
                            MealPlanID = mealPlanData.ID,
                            RoomPrice = Convert.ToDecimal(item["Room Price"]),
                            Price = Convert.ToDecimal(item["Price"]),
                            Discount = Convert.ToDecimal(item["Discount"]),
                            Total = Convert.ToDecimal(item["Total"]),
                            CurrencyID = currencyData.ID,
                            PromoCode = item["Promo Code"].ToString(),
                            //ReservationType = item["Reservation Type"].ToString()
                        };
                        _singleReservationService.Create(reservation);
                    }
                }
                _singleReservationService.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult _SingleResDetails(int singleResId)
        {
            var singleReservation = Mapper.Map<SingleReservation, SingleReservationVM>(_singleReservationService.GetSingleReservation(singleResId));
            return PartialView(singleReservation);
        }
    }
}