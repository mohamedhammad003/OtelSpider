using AutoMapper;
using OtelSpider.Core.BLL.Services;
using OtelSpider.Core.DAL.Models;
using OtelSpider.Web.ActionFilter;
using OtelSpider.Web.Helpers;
using OtelSpider.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OtelSpider.Web.Controllers
{
    public class HotelController : Controller
    {
        private readonly IHotelService _hotelService;
        private readonly IRoomTypeService _roomTypeService;
        private readonly IHotelOTAService _hotelOTAService;
        private readonly IOTAService _OTAService;
        private readonly IMealPlanService _mealPlanService;
        private readonly IOccupancyFormulaService _occupancyFormulaService;
        private List<int> roomMealPlans = new List<int>();

        private readonly string CurrentUserId;

        public HotelController(IHotelService hotelService, IRoomTypeService roomTypeService
            , IHotelOTAService hotelOTAService, IOTAService OTAService
            , IMealPlanService mealPlanService, IOccupancyFormulaService occupancyFormulaService)
        {
            _hotelService = hotelService;
            _roomTypeService = roomTypeService;
            _hotelOTAService = hotelOTAService;
            _OTAService = OTAService;
            _mealPlanService = mealPlanService;
            _occupancyFormulaService = occupancyFormulaService;
            CurrentUserId = UserHelper.Current.UserInfo.UserId;
        }

        #region Hotel Parent

        [AuthorizeRole(PermissionName = "SuperAdmin")]
        [HttpGet]
        public ActionResult Index()
        {
            var lstHotelParentModel = _hotelService.GetHotelParents();
            var lstHotelParents = Mapper.Map<IEnumerable<HotelParent>, IEnumerable<HotelParentViewModel>>(lstHotelParentModel);
            return View(lstHotelParents);
        }
        [AuthorizeRole(PermissionName = "SuperAdmin")]
        [HttpGet]
        public ActionResult CreateParent()
        {
            return View();
        }
        [AuthorizeRole(PermissionName = "SuperAdmin")]
        [HttpGet]
        public ActionResult EditParent(int Id)
        {
            var hotelParentModel = _hotelService.GetHotelParent(Id);
            var hotelParent = Mapper.Map<HotelParent, HotelParentViewModel>(hotelParentModel);

            return View(hotelParent);
        }
        [HttpPost]
        public ActionResult CreateParent(HotelParentViewModel hotelParent)
        {
            var hotelParentModel = Mapper.Map<HotelParentViewModel, HotelParent>(hotelParent);
            _hotelService.CreateHotelParent(hotelParentModel);
            _hotelService.SaveHotel();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult EditParent(HotelParentViewModel hotelParent)
        {
            var hotelParentModel = Mapper.Map<HotelParentViewModel, HotelParent>(hotelParent);
            _hotelService.UpdateHotelParent(hotelParentModel);
            _hotelService.SaveHotel();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteParent(int id)
        {
            _hotelService.DeleteHotelParent(id);
            _hotelService.SaveHotel();
            return RedirectToAction("Index");
        }
        #endregion

        #region Hotel Branch
        public ActionResult _HotelBranchs(int parentId)
        {
            var lstHotelModel = _hotelService.GetHotelsByParentId(parentId);
            var lstHotels = Mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelViewModel>>(lstHotelModel);
            ViewBag.HotelParentName = lstHotels.First().HotelParent.Name;
            return PartialView(lstHotels);
        }
        [HttpGet]
        public ActionResult NewBranch(int? parentId = null)
        {
            var lstParents = _hotelService.GetHotelParents();
            ViewBag.HotelParents = new SelectList(lstParents.Select(x =>
              new SelectListItem() { Text = x.Name.ToString(), Value = x.ID.ToString(), Selected = (parentId.HasValue && x.ID == parentId.Value) }), "Value", "Text");
            return View();
        }
        [HttpGet]
        public ActionResult EditHotelBranch(int id)
        {
            var hotelModel = _hotelService.GetHotel(id);
            var hotel = Mapper.Map<Hotel, HotelViewModel>(hotelModel);

            var lstParents = _hotelService.GetHotelParents();

            ViewBag.HotelParents = new SelectList(lstParents.Select(x =>
              new SelectListItem() { Text = x.Name.ToString(), Value = x.ID.ToString(), Selected = (x.ID == hotel.ParentID) }), "Value", "Text");

            return View(hotel);
        }
        [HttpPut]
        public ActionResult EditHotelBranch(HotelViewModel hotel)
        {
            Hotel hotelModel = Mapper.Map<HotelViewModel, Hotel>(hotel);
            _hotelService.UpdateHotel(hotelModel);
            _hotelService.SaveHotel();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult NewBranch(HotelViewModel hotel)
        {
            Hotel hotelModel = Mapper.Map<HotelViewModel, Hotel>(hotel);
            _hotelService.CreateHotel(hotelModel);
            _hotelService.SaveHotel();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteBranch(int id)
        {
            _hotelService.DeleteHotel(id);
            _hotelService.SaveHotel();
            return RedirectToAction("Index");
        }
        public ActionResult MyBranches()
        {
            var lstHotelModel = _hotelService.GetUserHotels(CurrentUserId);
            var lstHotels = Mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelViewModel>>(lstHotelModel);
            return View(lstHotels);

        }
        #endregion

        #region Room Types
        public ActionResult HotelRoomTypes()
        {
            var hotels = _hotelService.GetUserHotels(CurrentUserId);
            var lstRoomsModel = _roomTypeService.GetRoomTypesByHotels(hotels.Select(h => h.ID).ToList());
            var lstRoomsVM = Mapper.Map<IEnumerable<RoomType>, IEnumerable<RoomTypeViewModel>>(lstRoomsModel);
            return View(lstRoomsVM);
        }
        public ActionResult EditRoomType(int id)
        {
            var roomTypeVM = Mapper.Map<RoomType, RoomTypeViewModel>(_roomTypeService.GetRoomType(id));
            var lstHotels = _hotelService.GetUserHotels(CurrentUserId);

            ViewBag.MealPlans = Mapper.Map<IEnumerable<HotelMealPlan>, IEnumerable<HotelMealPlanVM>>(_mealPlanService.GetHotelMealPlansByHotelId(roomTypeVM.HotelID));
            ViewBag.lstHotels = new SelectList(lstHotels.Select(x =>
              new SelectListItem() { Text = x.Name.ToString(), Value = x.ID.ToString(), Selected = (x.ID == roomTypeVM.HotelID) }), "Value", "Text");
            return View(roomTypeVM);
        }
        public ActionResult CreateRoomType()
        {
            var lstHotels = _hotelService.GetUserHotels(CurrentUserId);
            ViewBag.MealPlans = Mapper.Map<IEnumerable<HotelMealPlan>, IEnumerable<HotelMealPlanVM>>(_mealPlanService.GetHotelMealPlansByHotelId(lstHotels.First().ID));
            ViewBag.lstHotels = new SelectList(lstHotels.Select(x =>
              new SelectListItem() { Text = x.Name.ToString(), Value = x.ID.ToString() }), "Value", "Text");
            return View();
        }
        [HttpPost]
        public ActionResult CreateRoomType(RoomTypeViewModel roomTypeVM)
        {

            if (!ModelState.IsValid)
            {
                return PartialView("_CreateRoomType");
            }
            var roomTypeModel = Mapper.Map<RoomTypeViewModel, RoomType>(roomTypeVM);

            _roomTypeService.Create(roomTypeModel);
            _roomTypeService.SaveChanges();
            return RedirectToAction("HotelRoomTypes", new { hotelID = roomTypeVM.HotelID });
        }
        [HttpPost]
        public ActionResult EditRoomType(RoomTypeViewModel roomTypeVM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_EditRoomType", new { id = roomTypeVM.ID });
            }
            var roomTypeModel = Mapper.Map<RoomTypeViewModel, RoomType>(roomTypeVM);

            _roomTypeService.Update(roomTypeModel);
            _roomTypeService.SaveChanges();
            return RedirectToAction("HotelRoomTypes", new { hotelID = roomTypeVM.HotelID });
        }
        public ActionResult DeleteRoomType(int id)
        {
            _roomTypeService.Delete(id);
            _roomTypeService.SaveChanges();
            return View();
        }
        #region Occupancy Formula
        public ActionResult RoomOccpancy(int roomId, int capacity)
        {
            ViewBag.Capacity = capacity;
            var formulaModel = _occupancyFormulaService.GetOccupancyFormula(roomId, capacity);
            if (formulaModel != null)
            {
                var formulaVM = Mapper.Map<OccupancyFormula, occupancyFormulaVM>(formulaModel);
                return PartialView("_EditOccupancy", formulaVM);
            }
            else
            {
                var formulaVM = new occupancyFormulaVM { Capacity = capacity, RoomTypeID = roomId };
                return PartialView("_CreateOccupancy", formulaVM);
            }
        }

        [HttpPost]
        public void UpdateBaseOccupancy(int baseCapacity, int roomID, int maxCapacity, int minCapacity)
        {
            if (baseCapacity > 0)
            {
                _occupancyFormulaService.DeleteRoomFormulas(roomID);
                for (int i = minCapacity; i <= maxCapacity; i++)
                {
                    var roomOccupancyFormula = new OccupancyFormula { Capacity = i, IsBase = i == baseCapacity, RoomTypeID = roomID };
                    _occupancyFormulaService.Create(roomOccupancyFormula);
                }
                _roomTypeService.SaveChanges();
            }
        }
        [HttpPost]
        public void CeateFormula(occupancyFormulaVM occupancyFormulaVM)
        {
            var formulaModel = Mapper.Map<occupancyFormulaVM, OccupancyFormula>(occupancyFormulaVM);
            _occupancyFormulaService.Create(formulaModel);
            _occupancyFormulaService.SaveChanges();
        }

        [HttpPost]
        public void UpdateFormula(occupancyFormulaVM occupancyFormulaVM)
        {
            var formulaModel = Mapper.Map<occupancyFormulaVM, OccupancyFormula>(occupancyFormulaVM);
            _occupancyFormulaService.Update(formulaModel);
            _occupancyFormulaService.SaveChanges();
        }

        #endregion

        [HttpPost]
        public void UpdateRoomMealPlans(List<int> mealIDs, int roomID)
        {
            _roomTypeService.DeleteRoomMealPlans(roomID);
            if (mealIDs != null)
            {
                foreach (var mealId in mealIDs)
                {
                    var roomMealPlan = new RoomMealPlan { MealPlanID = mealId, RoomTypeID = roomID };
                    _roomTypeService.AddRoomMealPlan(roomMealPlan);
                }
            }

            _roomTypeService.SaveChanges();
        }
        #endregion

        #region Hotel OTAs

        [HttpGet]
        public ActionResult HotelOTAs()
        {
            var userHotels = _hotelService.GetUserHotels(CurrentUserId);
            var lstOTAModel = _hotelOTAService.GetHotelOTAs(userHotels.Select(x => x.ID).ToList()); //_hotelService.GetHotelOTAsByUser(CurrentUserId);
            var lstOTAs = Mapper.Map<IEnumerable<HotelOTA>, IEnumerable<HotelOTAViewModel>>(lstOTAModel);
            return View(lstOTAs);
        }
        [HttpGet]
        public ActionResult AddHotelOTA()
        {
            var lstHotels = _hotelService.GetUserHotels(CurrentUserId);
            ViewBag.lstHotels = new SelectList(lstHotels.Select(x =>
              new SelectListItem() { Text = x.Name.ToString(), Value = x.ID.ToString() }), "Value", "Text");

            var lstOTAs = _OTAService.GetOTAs();
            ViewBag.lstOTAs = new SelectList(lstOTAs.Select(x =>
              new SelectListItem() { Text = x.Name.ToString(), Value = x.ID.ToString() }), "Value", "Text");

            return View();
        }
        [HttpGet]
        public ActionResult EditHotelOTA(int id)
        {
            var lstHotels = _hotelService.GetUserHotels(CurrentUserId);
            ViewBag.lstHotels = new SelectList(lstHotels.Select(x =>
              new SelectListItem() { Text = x.Name.ToString(), Value = x.ID.ToString() }), "Value", "Text");

            var lstOTAs = _OTAService.GetOTAs();
            ViewBag.lstOTAs = new SelectList(lstOTAs.Select(x =>
              new SelectListItem() { Text = x.Name.ToString(), Value = x.ID.ToString() }), "Value", "Text");

            var hotelOtaVM = Mapper.Map<HotelOTA, HotelOTAViewModel>(_hotelOTAService.GetHotelOTA(id));
            return View(hotelOtaVM);
        }

        [HttpPost]
        public ActionResult AddHotelOTA(HotelOTAViewModel hotelOTAView)
        {
            var hotelOtaModel = Mapper.Map<HotelOTAViewModel, HotelOTA>(hotelOTAView);
            _hotelOTAService.Create(hotelOtaModel);
            _hotelOTAService.SaveChanges();
            return RedirectToAction("HotelOTAs");
        }
        [HttpPost]
        public ActionResult EditHotelOTA(HotelOTAViewModel hotelOTAView)
        {
            var hotelOtaModel = Mapper.Map<HotelOTAViewModel, HotelOTA>(hotelOTAView);
            _hotelOTAService.Update(hotelOtaModel);
            _hotelOTAService.SaveChanges();
            return RedirectToAction("HotelOTAs");
        }
        public ActionResult DeleteHotelOTA(int id)
        {
            _hotelOTAService.Delete(id);
            _hotelOTAService.SaveChanges();
            return RedirectToAction("HotelOTAs");
        }
        #endregion

        #region Meal Plans
        public ActionResult HotelMealPlans()
        {
            var hotels = _hotelService.GetUserHotels(CurrentUserId);
            var lstMealPlansModel = _mealPlanService.GetMealPlansForHotels(hotels.Select(h => h.ID).ToList());
            var lstMealPlansVM = Mapper.Map<IEnumerable<HotelMealPlan>, IEnumerable<HotelMealPlanVM>>(lstMealPlansModel);
            return View(lstMealPlansVM);
        }

        public ActionResult _EditHotelMeals(int id)
        {
            var hotelMealVM = Mapper.Map<HotelMealPlan, HotelMealPlanVM>(_mealPlanService.GetHotelMealPlan(id));
            var lstMealPlans = _mealPlanService.GetMealPlans();

            var lstHotels = _hotelService.GetUserHotels(CurrentUserId);
            ViewBag.lstHotels = new SelectList(lstHotels.Select(x =>
              new SelectListItem() { Text = x.Name.ToString(), Value = x.ID.ToString(), Selected = (x.ID == hotelMealVM.HotelID) }), "Value", "Text");

            ViewBag.lstMealPlans = new SelectList(lstMealPlans.Select(x =>
              new SelectListItem() { Text = x.Name.ToString(), Value = x.ID.ToString() }), "Value", "Text");
            return PartialView(hotelMealVM);
        }
        public ActionResult _CreateHotelMeal()
        {
            var lstHotels = _hotelService.GetUserHotels(CurrentUserId);
            var lstMealPlans = _mealPlanService.GetMealPlans();

            ViewBag.lstHotels = new SelectList(lstHotels.Select(x =>
              new SelectListItem() { Text = x.Name.ToString(), Value = x.ID.ToString() }), "Value", "Text");

            ViewBag.lstMealPlans = new SelectList(lstMealPlans.Select(x =>
              new SelectListItem() { Text = x.Name.ToString(), Value = x.ID.ToString() }), "Value", "Text");
            return PartialView();
        }
        [HttpPost]
        public ActionResult CreateHotelMeal(HotelMealPlanVM hotelMealVM)
        {
            var hotelMealModel = Mapper.Map<HotelMealPlanVM, HotelMealPlan>(hotelMealVM);
            
            _mealPlanService.AddHotelMealPlan(hotelMealModel);
            _mealPlanService.SaveChanges();
            return RedirectToAction("HotelMealPlans", new { hotelID = hotelMealVM.HotelID });
        }
        [HttpPost]
        public ActionResult EditHotelMeal(HotelMealPlanVM hotelMealVM)
        {
            var hotelMealModel = Mapper.Map<HotelMealPlanVM, HotelMealPlan>(hotelMealVM);
            
            _mealPlanService.UpdateHotelMealPlan(hotelMealModel);
            _mealPlanService.SaveChanges();
            return RedirectToAction("HotelMealPlans", new { hotelID = hotelMealVM.HotelID });
        }
        public ActionResult DeletehotelMeal(int id)
        {
            _mealPlanService.DeleteHotelMealPlan(id);
            _mealPlanService.SaveChanges();
            return View();
        }
        #endregion

    }
}