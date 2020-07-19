using AutoMapper;
using OtelSpider.Core.BLL.Services;
using OtelSpider.Core.DAL.Models;
using OtelSpider.Web.ActionFilter;
using OtelSpider.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OtelSpider.Web.Controllers
{
    public class OTAController : Controller
    {
        private readonly IOTAService _otaService;

        public OTAController(IOTAService otaService)
        {
            _otaService = otaService;
        }
        #region Super Admin Views
        [AuthorizeRole(PermissionName = "SuperAdmin")]
        [HttpGet]
        public ActionResult Index()
        {
            var lstOTAModel = _otaService.GetOTAs();
            var lstOTAs = Mapper.Map<IEnumerable<OTA>, IEnumerable<OTAViewModel>>(lstOTAModel);
            return View(lstOTAs);
        }
        [AuthorizeRole(PermissionName = "SuperAdmin")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [AuthorizeRole(PermissionName = "SuperAdmin")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var otaModel = _otaService.GetOTA(id);
            var otaVM = Mapper.Map<OTA, OTAViewModel>(otaModel);
            return View(otaVM);
        }

        [HttpPost]
        public ActionResult Create(OTAViewModel otaView)
        {
            var OTAModel = Mapper.Map<OTAViewModel, OTA>(otaView);
            _otaService.CreateOTA(OTAModel);
            _otaService.SaveOTA();
            return RedirectToAction("Index");
        }
        [HttpPut]
        public ActionResult Edit(OTAViewModel otaView)
        {
            var OTAModel = Mapper.Map<OTAViewModel, OTA>(otaView);
            _otaService.Update(OTAModel);
            _otaService.SaveOTA();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            _otaService.DeleteOTA(id);
            _otaService.SaveOTA();
            return RedirectToAction("Index");
        }
        #endregion
    }
}