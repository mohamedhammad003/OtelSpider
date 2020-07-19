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
    [AuthorizeRole(PermissionName = "SuperAdmin")]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IOTAService _OTAService;
        private readonly IHotelService _hotelService;
        private readonly IBudgetService _budgetService;
        private readonly string CurrentUserId;

        public ReservationController(IReservationService reservationService
            , IOTAService OTAService
            , IHotelService hotelService
            , IBudgetService budgetService)
        {
            _reservationService = reservationService;
            _OTAService = OTAService;
            _hotelService = hotelService;
            _budgetService = budgetService;
            CurrentUserId = UserHelper.Current.UserInfo.UserId;
        }
        // GET: Reservation
        #region Annual Reservations
        [AuthorizeRole(PermissionName = "SuperAdmin")]
        public ActionResult Index()
        {
            var lstReservationModel = _reservationService.GetReservations();
            IEnumerable<MonthlyReservation> lstReservations = Mapper.Map<IEnumerable<MonthlyReservation>, IEnumerable<MonthlyReservation>>(lstReservationModel);
            var lstAnnualResr = lstReservations.GroupBy(x => new { x.HotelID, x.Year, x.OTAID })
                .Select(r => new AnnualReservationsViewModel
                {
                    HotelID = r.First().HotelID,
                    HotelName = r.First().Hotel.Name,
                    OTAID = r.First().OTAID,
                    OTAName = r.First().OTA.Name,
                    Year = r.First().Year,
                    RoomNights = r.Sum(n => n.RoomNights),
                    GrossRoomRevenue = r.Sum(n => n.GrossRoomRevenue),
                    NetRoomRevenue = r.Sum(n => n.NetRoomRevenue),
                    GrossARR = r.Sum(n => n.GrossARR),
                    NetARR = r.Sum(n => n.NetARR),
                    TargetBudget = _budgetService.GetTotalAnnualBudget(r.First().HotelID, r.First().Year),
                    TargetPrecentage = calculateBudgetPrecentage(r.Sum(n => n.GrossRoomRevenue), _budgetService.GetTotalAnnualBudget(r.First().HotelID, r.First().Year))
                });
            return View(lstAnnualResr);
        }
        public string getMonthlyReservations(int? hotelId = null, int? otaId = null, int? year = null)
        {
            return Url.Action("MonthlyReservations", new { hotelId = hotelId, otaId = otaId, year = year });
        }
        #endregion
        #region Monthly Reservations
        [HttpGet]
        public ActionResult MonthlyReservations(int? hotelId = null, int? otaId = null, int? year = null, int? month = null)
        {
            var viewModel = new MonthlyReservationsViewModel();

            if (!year.HasValue)
                year = viewModel.SelectedYear;
            if (!month.HasValue)
                month = viewModel.SelectedMonth;

            viewModel.monthlyReservations = Mapper.Map<IEnumerable<MonthlyReservation>, IEnumerable<MonthlyReservationVM>>(_reservationService.GetFilteredReservation(hotelId, otaId, year, month));
            viewModel.lstHotels = Mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelViewModel>>(_hotelService.GetUserHotels(CurrentUserId));
            viewModel.lstOTAs = Mapper.Map<IEnumerable<OTA>, IEnumerable<OTAViewModel>>(_OTAService.GetOTAs());

            foreach (var item in viewModel.monthlyReservations)
            {
                var budget = getMonthlyBudget(item.HotelID, item.Year, item.Month);
                item.TargetBudget = budget;
                item.TargetPrecentage = calculateBudgetPrecentage(item.GrossRoomRevenue, budget);
            }
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
        public ActionResult FilteredMonthlyReservations(int? hotelId = null, int? otaId = null, int? year = null, int? month = null)
        {
            var lstReservationModel = _reservationService.GetFilteredReservation(hotelId, otaId, year, month);
            IEnumerable<MonthlyReservationVM> lstReservations = Mapper.Map<IEnumerable<MonthlyReservation>, IEnumerable<MonthlyReservationVM>>(lstReservationModel);
            return Json(lstReservations, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult _GetMonthlyResList(IEnumerable<MonthlyReservationVM> monthlyReservations)
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult uploadMonthlyReport(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);

                var jsonReport = ExcelToJson.ConvertExcelToJson(path);
                List<string> uploadErrors = new List<string>();
                foreach (var item in jsonReport)
                {
                    var otaData = _OTAService.GetOTAByName(item["OTA"].ToString());
                    var hotelData = _hotelService.GetHotelByName(item["Hotel"].ToString());

                    int month = DateTime.ParseExact(item["Month"].ToString(), "MMMM", new CultureInfo("en-US")).Month;
                    int roomNights = Convert.ToInt32(item["Room Nights"]);

                    decimal grossRoomRevenue = Convert.ToDecimal(item["Gross Room Revenue"]);
                    decimal grossARR = item["Net Room Revenue"] != null ? Convert.ToDecimal(item["Gross ARR"]) : (grossRoomRevenue / roomNights);
                    decimal netRoomRevenue = Convert.ToDecimal(item["Net Room Revenue"]);
                    decimal netARR = item["Net ARR"] != null ? Convert.ToDecimal(item["Net ARR"]) : (netRoomRevenue / roomNights);

                    if (otaData == null)
                    {
                        uploadErrors.Add("there is no OTA with the Name : " + item["OTA"].ToString());
                        continue;
                    }
                    if (hotelData == null)
                    {
                        uploadErrors.Add("there is no Hotel with the Name : " + item["Hotel"].ToString());
                        continue;
                    }

                    var reservationModels = _reservationService.GetFilteredReservation(hotelData.ID, otaData.ID, Convert.ToInt32(item["Year"]), month);
                    var monthlyReservation = reservationModels != null ? reservationModels.FirstOrDefault() : null;

                    if (monthlyReservation != null)
                    {
                        monthlyReservation.OTAID = otaData.ID;
                        monthlyReservation.HotelID = hotelData.ID;
                        monthlyReservation.Year = Convert.ToInt32(item["Year"]);
                        monthlyReservation.Month = month;
                        monthlyReservation.MonthName = item["Month"].ToString();
                        monthlyReservation.RoomNights = roomNights;
                        monthlyReservation.GrossRoomRevenue = grossRoomRevenue;
                        monthlyReservation.GrossARR = grossARR;
                        monthlyReservation.NetRoomRevenue = netRoomRevenue;
                        monthlyReservation.NetARR = netARR;
                        monthlyReservation.LastUpdateDate = DateTime.Now;

                        _reservationService.UpdateMonthlyReservation(monthlyReservation);
                    }
                    else
                    {
                        var reservation = new Core.DAL.Models.MonthlyReservation()
                        {
                            OTAID = otaData.ID,
                            HotelID = hotelData.ID,
                            Year = Convert.ToInt32(item["Year"]),
                            Month = month,
                            MonthName = item["Month"].ToString(),
                            RoomNights = roomNights,
                            GrossRoomRevenue = grossRoomRevenue,
                            GrossARR = grossARR,
                            NetRoomRevenue = netRoomRevenue,
                            NetARR = netARR,
                            LastUpdateDate = DateTime.Now,
                            CreationDate = DateTime.Now
                        };

                        _reservationService.CreateMonthlyReservation(reservation);
                    }
                }

                _reservationService.SaveReservation();
            }

            return RedirectToAction("Index");
        }
        #endregion
        #region Daily Reservations
        public string getDailyReservations(int? hotelId = null, int? otaId = null, int? year = null, int? month = null)
        {
            return Url.Action("DailyReservations", new { hotelId = hotelId, otaId = otaId, year = year, month = month });
        }
        [AuthorizeRole(PermissionName = "SuperAdmin")]
        [HttpGet]
        public ActionResult DailyReservations(int? hotelId = null, int? otaId = null, int? year = null, int? month = null)
        {
            var viewModel = new DailyReservationViewModel();

            if (!year.HasValue)
                year = viewModel.SelectedYear;
            if (!month.HasValue)
                month = viewModel.SelectedMonth;

            viewModel.dailyReservations = Mapper.Map<IEnumerable<DailyReservation>, IEnumerable<DailyReservationVM>>(_reservationService.GetFilteredDailyReservation(hotelId, otaId, year, month));
            viewModel.lstHotels = Mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelViewModel>>(_hotelService.GetUserHotels(CurrentUserId)); ;
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
        public ActionResult FilteredDailyReservations(int? hotelId = null, int? otaId = null, int? year = null, int? month = null)
        {
            var lstReservationModel = _reservationService.GetFilteredDailyReservation(hotelId, otaId, year, month);
            IEnumerable<DailyReservationVM> lstReservations = Mapper.Map<IEnumerable<DailyReservation>, IEnumerable<DailyReservationVM>>(lstReservationModel);
            return Json(lstReservations, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult _GetDailyResList(IEnumerable<DailyReservationVM> dailyReservations)
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult uploadDailyReport(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads/DailyReservations"), fileName);
                file.SaveAs(path);

                var jsonReport = ExcelToJson.ConvertExcelToJson(path);
                List<string> uploadErrors = new List<string>();
                foreach (var item in jsonReport)
                {
                    var otaData = _OTAService.GetOTAByName(item["OTA"].ToString());
                    var hotelData = _hotelService.GetHotelByName(item["Hotel"].ToString());

                    DateTime date = Convert.ToDateTime(item["Date"]);
                    int roomNights = Convert.ToInt32(item["Room Nights"]);

                    decimal grossRoomRevenue = Convert.ToDecimal(item["Gross Room Revenue"]);
                    decimal grossARR = item["Net Room Revenue"] != null ? Convert.ToDecimal(item["Gross ARR"]) : (grossRoomRevenue / roomNights);
                    decimal netRoomRevenue = Convert.ToDecimal(item["Net Room Revenue"]);
                    decimal netARR = item["Net ARR"] != null ? Convert.ToDecimal(item["Net ARR"]) : (netRoomRevenue / roomNights);


                    if (otaData == null)
                    {
                        uploadErrors.Add("there is no OTA with the Name : " + item["OTA"].ToString());
                        continue;
                    }
                    if (hotelData == null)
                    {
                        uploadErrors.Add("there is no Hotel with the Name : " + item["Hotel"].ToString());
                        continue;
                    }

                    var reservationModels = _reservationService.GetFilteredDailyReservation(hotelData.ID, otaData.ID, Convert.ToDateTime(item["Date"]));
                    var dailyReservation = reservationModels != null ? reservationModels.FirstOrDefault() : null;

                    if (dailyReservation != null)
                    {
                        dailyReservation.OTAID = otaData.ID;
                        dailyReservation.HotelID = hotelData.ID;
                        dailyReservation.ReservationDay = Convert.ToDateTime(item["Date"]);
                        dailyReservation.RoomNights = roomNights;
                        dailyReservation.GrossRoomRevenue = grossRoomRevenue;
                        dailyReservation.GrossARR = grossARR;
                        dailyReservation.NetRoomRevenue = netRoomRevenue;
                        dailyReservation.NetARR = netARR;
                        dailyReservation.LastUpdateDate = DateTime.Now;
                        _reservationService.UpdateDailyReservation(dailyReservation);
                    }
                    else
                    {

                        var reservation = new DailyReservation()
                        {
                            OTAID = otaData.ID,
                            HotelID = hotelData.ID,
                            ReservationDay = Convert.ToDateTime(item["Date"]),
                            RoomNights = roomNights,
                            GrossRoomRevenue = grossRoomRevenue,
                            GrossARR = grossARR,
                            NetRoomRevenue = netRoomRevenue,
                            NetARR = netARR,
                            LastUpdateDate = DateTime.Now,
                            CreationDate = DateTime.Now
                        };
                        _reservationService.CreateDailyReservation(reservation);
                    }
                }

                _reservationService.SaveReservation();
            }

            return RedirectToAction("Index");
        }
        #endregion
        private decimal calculateBudgetPrecentage(decimal income, decimal budget)
        {
            return budget > 0 ? (income / budget) * 100 : 0;
        }
        private decimal getMonthlyBudget(int hotelId, int Year, int Month)
        {
            var annualBudget = _budgetService.GetYearBudgets(hotelId, Year);
            if (annualBudget != null)
            {
                switch (Month)
                {
                    case 1:
                        return annualBudget.January;
                    case 2:
                        return annualBudget.February;
                    case 3:
                        return annualBudget.March;
                    case 4:
                        return annualBudget.April;
                    case 5:
                        return annualBudget.May;
                    case 6:
                        return annualBudget.June;
                    case 7:    
                        return annualBudget.July;
                    case 8:    
                        return annualBudget.August;
                    case 9:    
                        return annualBudget.September;
                    case 10:   
                        return annualBudget.October;
                    case 11:   
                        return annualBudget.November;
                    case 12:   
                        return annualBudget.December;
                    default:
                        return 0;
                }
            }
            return 0;
        }
    }
}