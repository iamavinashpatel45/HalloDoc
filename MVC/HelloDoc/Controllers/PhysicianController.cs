using AspNetCoreHero.ToastNotification.Abstractions;
using HelloDoc.Authentication;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.AdminServices;
using Services.Interfaces.PhysicianServices;
using Services.ViewModels.Admin;
using Services.ViewModels.Physician;

namespace HelloDoc.Controllers
{
    public class PhysicianController : Controller
    {

        private readonly INotyfService _notyfService;
        private readonly IPhysicianDashboardService _physicianDashboardService;
        private readonly IAdminDashboardService _adminDashboardService;

        public PhysicianController(INotyfService notyfService, IPhysicianDashboardService physicianDashboardService,
                                                  IAdminDashboardService adminDashboardService)
        {
            _notyfService = notyfService;
            _physicianDashboardService = physicianDashboardService;  
            _adminDashboardService = adminDashboardService; 
        }

        [Authorization("Physician")]
        public IActionResult Dashboard()
        {
            int aspNetUserId = HttpContext.Session.GetInt32("aspNetUserId").Value;
            return View(_physicianDashboardService.getallRequests(aspNetUserId));
        }

        [HttpGet]   // Dashboard 
        public IActionResult GetTablesData(String status, int pageNo, String partialViewName, String patinetName, int regionId, int requesterTypeId)
        {
            int aspNetUserId = HttpContext.Session.GetInt32("aspNetUserId").Value;
            TableModel tableModel = _physicianDashboardService.GetNewRequest(status, pageNo, patinetName, regionId, requesterTypeId, aspNetUserId);
            return tableModel.TableDatas.Count != 0 ? PartialView(partialViewName, tableModel) : PartialView("_NoTableDataFound");
        }

        public IActionResult Scheduling()
        {
            int aspNetUserId = HttpContext.Session.GetInt32("aspNetUserId").Value;
            return View(_physicianDashboardService.providerScheduling(aspNetUserId));
        }

        public async Task<IActionResult> AcceptRequest(int requestId)
        {
            if (await _physicianDashboardService.acceptRequest(requestId))
            {
                _notyfService.Success("Successfully Accepted");
            }
            else
            {
                _notyfService.Error("Faild !!");
            }
            return RedirectToAction("Dashboard", "Physician");
        }

        [HttpPost]
        public async Task<IActionResult> TransferRequest(PhysicianTransferRequest model)
        {
            if (await _physicianDashboardService.transferRequest(model))
            {
                _notyfService.Success("Successfully Transfered");
            }
            else
            {
                _notyfService.Error("Faild !!");
            }
            return RedirectToAction("Dashboard", "Physician");
        }
            
        [HttpGet]
        public async Task<JsonResult> SetEncounter(bool isVideoCall,int requestId)
        {
            await _physicianDashboardService.setEncounter(requestId, isVideoCall);
            return Json(new { redirect = Url.Action("Dashboard", "Physician") });
        }

        public async Task<IActionResult> HomeVisit(int requestId)
        {
            if (await _physicianDashboardService.setEncounter(requestId,true))
            {
                HttpContext.Session.SetInt32("requestId", requestId);
                return RedirectToAction("EncounterForm", "Admin");
            }
            else
            {
                return RedirectToAction("Dashboard", "Physician");
            }
        }

        public IActionResult ViewProfile()
        {
            int aspNetUserId = HttpContext.Session.GetInt32("aspNetUserId").Value;
            int PhysicianId = _physicianDashboardService.getPhysicianIdFromAspNetUserId(aspNetUserId);
            return RedirectToAction("SetPhyscianId", "Admin", new { physicianId = PhysicianId });
        }

        public async Task<IActionResult> FinalizeEncounter(EncounterForm model)
        {
            int requestId = HttpContext.Session.GetInt32("requestId").Value;
            int aspNetUserId = HttpContext.Session.GetInt32("aspNetUserId").Value;
            model.IsFinalize = true;
            if (await _adminDashboardService.updateEncounter(model, requestId, aspNetUserId))
            {
                _notyfService.Success("Successfully Updated");
            }
            else
            {
                _notyfService.Error("Update Faild !!");
            }
            return RedirectToAction("Dashboard", "Physician");
        }

        [HttpGet]    // provider scheduling page 
        public IActionResult GetMonthWiseData(String time)
        {
            int aspNetUserId = HttpContext.Session.GetInt32("aspNetUserId").Value;
            return PartialView("_monthWiseScheduling", _physicianDashboardService.monthWiseScheduling(time, aspNetUserId));
        }

        public IActionResult GeneratePDF(int requestId)
        {
            byte[] bytes = _physicianDashboardService.generateMedicalReport(requestId);
            return File(bytes, "application/pdf", "MedicalReport.pdf");
        }

        [HttpPost("/Case/FinalConcludeCare")]
        public async Task<IActionResult> FinalConcludeCare(ConcludeCare model)
        {
            int requestId = HttpContext.Session.GetInt32("requestId").Value;
            if(await _physicianDashboardService.concludeCare(requestId, model))
            {
                _notyfService.Success("Successfully Concluded");
                return RedirectToAction("Dashboard", "Physician");
            }
            else
            {
                _notyfService.Warning("Encounter Is not Finalize");
                return RedirectToAction("EncounterForm", "Admin");
            }
        }

    }
}
