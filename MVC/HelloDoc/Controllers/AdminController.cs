using AspNetCoreHero.ToastNotification.Abstractions;
using ClosedXML.Excel;
using HelloDoc.Authentication;
using Microsoft.AspNetCore.Mvc;
using Repositories.DataModels;
using Services.Interfaces;
using Services.Interfaces.AdminServices;
using Services.Interfaces.AuthServices;
using Services.ViewModels;
using Services.ViewModels.Admin;
using System.Data;

namespace HelloDoc.Controllers
{
    public class AdminController : Controller
    {
        private readonly INotyfService _notyfService;
        private readonly ILoginService _loginService;
        private readonly IAdminDashboardService _adminDashboardService;
        private readonly IViewNotesService _viewNotesService;
        private readonly IViewDocumentsServices _viewDocumentsServices;
        private readonly IJwtService _jwtService;
        private readonly ISendOrderService _sendOrderService;
        private readonly ICloseCaseService _closeCaseService;
        private readonly IViewProfileService _viewProfileService;
        private readonly IProvidersService _providersService;
        private readonly IAccessService _accessService;
        private readonly IPartnersService _partnersService;
        private readonly IRecordService _recordService;
        private List<string> roles = new List<string> { "Admin", "Physician" };

        public AdminController(INotyfService notyfService, IAdminDashboardService adminDashboardService, 
                                IViewNotesService viewNotesService, ILoginService loginService, IViewDocumentsServices viewDocumentsServices,
                                IJwtService jwtService, ISendOrderService sendOrderService, ICloseCaseService closeCaseService, 
                                IViewProfileService viewProfileService, IPartnersService partnersService,
                                IProvidersService providersService, IAccessService accessService, IRecordService recordService)
        {
            _notyfService = notyfService;
            _loginService = loginService;
            _adminDashboardService = adminDashboardService;
            _viewNotesService = viewNotesService;
            _viewDocumentsServices = viewDocumentsServices;
            _jwtService = jwtService;
            _partnersService = partnersService;
            _sendOrderService = sendOrderService;
            _closeCaseService = closeCaseService;
            _viewProfileService = viewProfileService;
            _providersService = providersService;
            _accessService = accessService;
            _recordService = recordService;
        }

        public IActionResult LoginPage()
        {
            string role = _loginService.isTokenValid(HttpContext, new List<int> { 2, 3 });
            if (role != null)
            {
                return RedirectToAction("Dashboard", role);
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("aspNetUserId");
            HttpContext.Session.Remove("role");
            Response.Cookies.Delete("jwtToken");
            return RedirectToAction("LoginPage", "Admin");
        }

        [Authorization("Admin")]
        public IActionResult Dashboard()
        {
            return View(_adminDashboardService.getallRequests());
        }

        [Authorization("Admin", "Physician")]
        [HttpGet("/Case/ConcludeCare")]
        public IActionResult ConcludeCare()
        {
            int requestId = HttpContext.Session.GetInt32("requestId").Value;
            return View(_adminDashboardService.getConcludeCare(requestId));
        }

        [Authorization("Admin", "Physician")]
        [HttpGet("/Encounter/EncounterForm")]
        public IActionResult EncounterForm()
        {
            int requestId = HttpContext.Session.GetInt32("requestId").Value;
            bool isAdmin = HttpContext.Session.GetString("role") == "Admin";
            return View(_adminDashboardService.getEncounterDetails(requestId, isAdmin));
        }
         
        [Authorization("Admin", "Physician")]
        [HttpGet("/Case/ViewCase")]
        public IActionResult ViewCase()
        {
            int requestId = HttpContext.Session.GetInt32("requestId").Value;
            bool isAdmin = HttpContext.Session.GetString("role") == "Admin";
            return View(_adminDashboardService.getRequestDetails(requestId,isAdmin));
        }

        [Authorization("Admin", "Physician")]
        [HttpGet("/Case/ViewNotes")]
        public IActionResult ViewNotes()
        {
            int requestId = HttpContext.Session.GetInt32("requestId").Value;
            return View(_viewNotesService.GetNotes(requestId));
        }

        [Authorization("Admin", "Physician")]
        [HttpGet("/Request/CreateRequest")]
        public IActionResult CreateRequest()
        {
            return View(null);
        }

        [Authorization("Admin")]
        public IActionResult CreateProvider()
        {
            return View(_providersService.getCreateProvider());
        }

        [Authorization("Admin")]
        public IActionResult CreateAdmin()
        {
            return View(_accessService.GetAdminCreaateAndProfile());
        }

        [Authorization("Admin")]
        public IActionResult Providers()
        {
            return View(_providersService.getProviders(regionId: 0));
        }

        [Authorization("Admin")]
        public IActionResult Partners()
        {
            return View(_partnersService.getPartnersData());
        }

        [Authorization("Admin")]
        public IActionResult BusinessProfile()
        {
            return View(_partnersService.addBusiness(isUpdate: false,venderId: 0));
        }

        [Authorization("Admin")]
        public IActionResult Access()
        {
            return View(_accessService.getAccessData());
        }

        [Authorization("Admin")]
        public IActionResult ProviderOnCall()
        {
            return View(_providersService.getProviderOnCall(regionId: 0));
        }

        [Authorization("Admin")]
        public IActionResult CreateRole()
        {
            return View(_accessService.getCreateRole());
        }

        [Authorization("Admin")]
        public IActionResult Records()
        {
            return View(_recordService.getRecords(new Records()));
        }

        [Authorization("Admin")]
        public IActionResult EmailLogs()
        {
            return View(_recordService.getEmailLog(new EmailSmsLogs()));
        }

        [Authorization("Admin")]
        public IActionResult SMSLogs()
        {
            return View(_recordService.getSMSlLog(new EmailSmsLogs()));
        }

        [Authorization("Admin")]
        public IActionResult PatientHistory()
        {
            return View(_recordService.getPatientHistory(new PatientHistory(),pageNo:1));
        }

        [Authorization("Admin")]
        public IActionResult PatientRecord()
        {
            int userId = HttpContext.Session.GetInt32("userId").Value;
            return View(_recordService.getPatientRecord(userId,pageNo: 1));
        }

        [Authorization("Admin")]
        public IActionResult BlockHistory()
        {
            return View(_recordService.getBlockHistory(new BlockHistory(), pageNo: 1));
        }

        [Authorization("Admin")]
        public IActionResult ViewProfile()
        {
            int aspNetUseId = HttpContext.Session.GetInt32("aspNetUserId").Value;
            return View(_viewProfileService.GetAdminViewProfile(aspNetUseId));
        }

        [Authorization("Admin")]
        public IActionResult ProviderScheduling()
        {
            return View(_providersService.getProviderSchedulingData());
        }

        [Authorization("Admin")]
        public IActionResult RequestedShift()
        {
            return View(_providersService.getRequestedShift());
        }

        [Authorization("Admin")]
        public IActionResult CloseCase()
        {
            int requestId = HttpContext.Session.GetInt32("requestId").Value;
            return View(_closeCaseService.getDaetails(requestId));
        }

        [Authorization("Admin")]
        public IActionResult ProviderLocation()
        {
            return View();
        }

        [Authorization("Admin", "Physician")]
        [HttpGet("/Case/ViewDocument")]
        public IActionResult ViewDocument()
        {
            int aspNetUserId = HttpContext.Session.GetInt32("aspNetUserId").Value;
            int requestId = HttpContext.Session.GetInt32("requestId").Value;
            return View(_viewDocumentsServices.getDocumentList(requestId: requestId, aspNetUserId: aspNetUserId));
        }

        [Authorization("Admin", "Physician")]
        [HttpGet("/Order/SendOrder")]
        public IActionResult SendOrder()
        {
            int requestId = HttpContext.Session.GetInt32("requestId").Value;
            return View(_sendOrderService.getSendOrderDetails(requestId));
        }

        [Authorization("Admin")]
        public IActionResult UpdateBusiness(int venderId)
        {
            HttpContext.Session.SetInt32("venderId", venderId);
            return View("BusinessProfile", _partnersService.addBusiness(isUpdate: true, venderId: venderId));
        }

        [Authorization("Admin")]
        public IActionResult UpdateRole(int roleId)
        {
            HttpContext.Session.SetInt32("roleId", roleId);
            return View("CreateRole", _accessService.getEditRole(roleId));
        }

        [Authorization("Admin", "Physician")]
        public IActionResult SetPhyscianId(int physicianId)
        {
            HttpContext.Session.SetInt32("physicianId", physicianId);
            return RedirectToAction("EditProvider", "Admin");
        }

        [Authorization("Admin", "Physician")]
        [HttpGet("/Profile/ViewProfile")]
        public IActionResult EditProvider()
        {
            int physicianId = HttpContext.Session.GetInt32("physicianId").Value;
            return View(_providersService.getEditProvider(physicianId));
        }

        public IActionResult Agreement(String token)
        {
            Agreement agreement = _adminDashboardService.getUserDetails(token);
            if (agreement.IsValid)
            {
                return View(agreement);
            }
            _notyfService.Error(agreement.Message);
            return RedirectToAction("PatientSite", "Patient");
        }

        public async Task<JsonResult> DeleteAllFiles(String requestWiseFileIdsList)   // delete all seleted file - view documents
        {
            int requestId = HttpContext.Session.GetInt32("requestId").Value;
            if (await _viewDocumentsServices.deleteAllFile(requestWiseFileIdsList, requestId))
            {
                _notyfService.Success("Successfully File Deleted");
            }
            else
            {
                _notyfService.Error("Faild!");
            }
            return Json(new { redirect = Url.Action("ViewDocument", "Admin") });
        }

        public async Task<IActionResult> DeleteFile(int requestWiseFileId)   /// delete perticuler one file - view documents
        {
            if (await _viewDocumentsServices.deleteFile(requestWiseFileId))
            {
                _notyfService.Success("Successfully File Deleted");
            }
            else
            {
                _notyfService.Error("Faild!");
            }
            return RedirectToAction("ViewDocument", "Admin");
        }

        public IActionResult SendMail(String requestWiseFileIdsList)  /// send mail from view document
        {
            int requestId = HttpContext.Session.GetInt32("requestId").Value;
            if (_viewDocumentsServices.sendFileMail(requestWiseFileIdsList, requestId))
            {
                _notyfService.Success("Successfully Send Mail");
            }
            else
            {
                _notyfService.Error("Faild!");
            }
            return Json(new { redirect = Url.Action("ViewDocument", "Admin") });
        }

        public async Task<IActionResult> CancelPopUp(CancelPopUp model)
        {
            if (await _viewNotesService.cancleRequest(model))
            {
                _notyfService.Success("Successfully Reuqest Cancel");
            }
            else
            {
                _notyfService.Error("Request Cancel Faild!");
            }
            return RedirectToAction("Dashboard", "Admin");
        }

        public async Task<IActionResult> AssignPopUp(AssignAndTransferPopUp model)
        {
            if (await _viewNotesService.assignRequest(model))
            {
                _notyfService.Success("Successfully Reuqest Assign");
            }
            else
            {
                _notyfService.Error("Request Assign Faild!");
            }
            return RedirectToAction("Dashboard", "Admin");
        }

        public async Task<IActionResult> TransferPopUp(AssignAndTransferPopUp model)
        {
            if (ModelState.IsValid)
            {
                if (await _viewNotesService.assignRequest(model))
                {
                    _notyfService.Success("Successfully Reuqest Transfer");
                }
                else
                {
                    _notyfService.Error("Request Transfer Faild!");
                }
                return RedirectToAction("Dashboard", "Admin");
            }
            return View(model);
        }

        public async Task<IActionResult> BlockPopUp(BlockPopUp model)
        {
            if (ModelState.IsValid)
            {
                if (await _viewNotesService.blockRequest(model))
                {
                    _notyfService.Success("Successfully Reuqest Block");
                }
                else
                {
                    _notyfService.Error("Request Block Faild!");
                }
                return RedirectToAction("Dashboard", "Admin");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<JsonResult> ClearPopUp(int requestId)
        {
            if (await _viewNotesService.clearRequest(requestId))
            {
                _notyfService.Success("Successfully Reuqest Clear");
            }
            else
            {
                _notyfService.Error("Request Clear Faild !!");
            }
            return Json(new { redirect = Url.Action("Dashboard", "Admin") });
        }

        public IActionResult SendAgreementPopUp(Agreement model)
        {
            if (ModelState.IsValid)
            {
                if (_viewNotesService.sendAgreement(model, HttpContext))
                {
                    _notyfService.Success("Successfully Send");
                }
                else
                {
                    _notyfService.Error("Agreement Send Faild!");
                }
                string role = HttpContext.Session.GetString("role");
                return RedirectToAction("Dashboard", role);
            }
            return View(model);
        }

        public IActionResult RequestSupport(RequestSupport model)  //// request support on dashboard
        {
            if (ModelState.IsValid)
            {
                if (_adminDashboardService.RequestSupport(model))
                {
                    _notyfService.Success("Successfully Send");
                }
                else
                {
                    _notyfService.Error("Faild!");
                }
                return RedirectToAction("Dashboard", "Admin");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShift(CreateShift model)  //// request support on dashboard
        {
            if (ModelState.IsValid)
            {
                int aspNetUseId = HttpContext.Session.GetInt32("aspNetUserId").Value;
                bool isAdmin = HttpContext.Session.GetString("role") == "Admin";
                if (await _providersService.createShift(model, aspNetUseId, isAdmin))
                {
                    _notyfService.Success("Successfully Created");
                }
                else
                {
                    _notyfService.Error("Faild!");
                }
                return isAdmin ? RedirectToAction("ProviderScheduling", "Admin") : RedirectToAction("Scheduling", "Physician");
            }
            return View(model);
        }

        public async Task<IActionResult> AgreementAgree(Agreement model)
        {
            if (await _viewNotesService.agreementAgree(model))
            {
                _notyfService.Success("Successfully Agreed");
            }
            else
            {
                _notyfService.Error("Faild!");
            }
            return RedirectToAction("PatientSite", "Patient");
        }

        public async Task<IActionResult> AgreementDeclined(Agreement model)
        {
            if (await _viewNotesService.agreementDeclined(model))
            {
                _notyfService.Success("Successfully Declined");
            }
            else
            {
                _notyfService.Error("Faild!");
            }
            return RedirectToAction("PatientSite", "Patient");
        }

        [HttpGet]  /// set requestId in session
        public JsonResult SetRequestId(int requestId, String actionName)
        {
            HttpContext.Session.SetInt32("requestId", requestId);
            return Json(new { redirect = Url.Action(actionName, "Admin") });
        }

        [HttpGet]  /// Navigation to Patient Records
        public JsonResult GetRecords(int userId)
        {
            HttpContext.Session.SetInt32("userId", userId);
            return Json(new { redirect = Url.Action("PatientRecord", "Admin") });
        }

        [HttpGet]  ////  SendAgreementPopUp
        public JsonResult GetEmailAndMobileNumber(int requestId)
        {
            return Json(_adminDashboardService.getRequestClientEmailAndMobile(requestId));
        }

        [HttpGet] //// Assigncase and TransfercasePopUp
        public JsonResult GetPhysicians(int regionId)
        {
            return Json(_adminDashboardService.getPhysiciansByRegion(regionId));
        }

        [HttpGet] //// Send Order
        public JsonResult GetBussinesses(int professionId)
        {
            return Json(_sendOrderService.getBussinessByProfession(professionId));
        }

        [HttpGet] //// Region Filter on Provider page
        public IActionResult RegionFilter(int regionId)
        {
            return PartialView("_ProviderTable", _providersService.getProviders(regionId: regionId).providers);
        }

        [HttpGet] //// change checkboc menus on create role page
        public IActionResult ChangeMenusByRole(int roleId)
        {
            return PartialView("_CreateRoleCheckBox", _accessService.getMenusByRole(roleId));
        }

        [HttpPost] //// create roles
        public async Task<IActionResult> CreateRole(CreateRole model)
        {
            if (await _accessService.createRole(model))
            {
                _notyfService.Success("Successfully Role Created");
            }
            else
            {
                _notyfService.Error("Faild !!");
            }
            return RedirectToAction("Access", "Admin");
        }

        [HttpPost] //// update roles
        public async Task<IActionResult> UpdateRole(CreateRole model)
        {
            int roleId = HttpContext.Session.GetInt32("roleId").Value;
            int aspNetUserId = HttpContext.Session.GetInt32("aspNetUserId").Value;
            if (await _accessService.editRole(model,roleId,aspNetUserId))
            {
                _notyfService.Success("Successfully Role Updated");
            }
            else
            {
                _notyfService.Error("Faild !!");
            }
            return RedirectToAction("Access", "Admin");
        }

        public async Task<IActionResult> DeteteRole(int roleId)  ////   delete role - access page
        {
            int aspNetUserId = HttpContext.Session.GetInt32("aspNetUserId").Value;
            if (await _accessService.delete(roleId,aspNetUserId))
            {
                _notyfService.Success("Successfully Role Deleted");
            }
            else
            {
                _notyfService.Error("Faild !!");
            }
            return RedirectToAction("Access", "Admin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  /////  sendlink ---  Dashboard
        public IActionResult SendLink(SendLink model)
        {
            if (_adminDashboardService.SendRequestLink(model, HttpContext))
            {
                _notyfService.Success("Successfully Link Send");
            }
            else
            {
                _notyfService.Error("Link Send Faild !!");
            }
            string role = HttpContext.Session.GetString("role");
            return RedirectToAction("Dashboard", role);
        }

        [HttpPost("/Request/CreateRequest")]
        [ValidateAntiForgeryToken]  /////  create request ---  Dashboard
        public async Task<IActionResult> CreateRequest(CreateRequest model)
        {
            int aspNetUserId = HttpContext.Session.GetInt32("aspNetUserId").Value;
            bool isAdmin = HttpContext.Session.GetString("role") == "Admin";
            if (await _adminDashboardService.createRequest(model, aspNetUserId, isAdmin))
            {
                _notyfService.Success("Successfully Request Added");
            }
            else
            {
                _notyfService.Error("Faild!");
            }
            string role = HttpContext.Session.GetString("role");
            return RedirectToAction("Dashboard", role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  /////  create provider ---  provider page
        public async Task<IActionResult> CreateProvider(CreateProvider model)
        {
            if (ModelState.IsValid)
            {
                if (await _providersService.createProvider(model))
                {
                    _notyfService.Success("Successfully Provider Created");
                }
                else
                {
                    _notyfService.Error("Faild!");
                }
                return RedirectToAction("Providers", "Admin");
            }
            CreateProvider createProvider = _providersService.getCreateProvider();
            model.Roles = createProvider.Roles;
            model.Regions = createProvider.Regions;
            _notyfService.Warning("Add Required Field.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  /////  create Admin ---  dashboard 
        public async Task<IActionResult> CreateAdmin(AdminCreaateAndProfile model)
        {
            if (ModelState.IsValid)
            {
                if (await _accessService.createAdmin(model))
                {
                    _notyfService.Success("Successfully Admin Created");
                }
                else
                {
                    _notyfService.Error("Faild!");
                }
                return RedirectToAction("Dashboard", "Admin");
            }
            AdminCreaateAndProfile adminCreaateAndProfile = _accessService.GetAdminCreaateAndProfile();
            model.Roles = adminCreaateAndProfile.Roles;
            model.Regions = adminCreaateAndProfile.Regions;
            _notyfService.Warning("Add Required Field.");
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Case/ViewDocument")]
        public async Task<IActionResult> ViewDocument(ViewDocument model)
        {
            String firstname = HttpContext.Session.GetString("firstName");
            String lastName = HttpContext.Session.GetString("lastName");
            int requestId = HttpContext.Session.GetInt32("requestId").Value;
            if (await _viewDocumentsServices.uploadFile(model, firstname, lastName, requestId))
            {
                _notyfService.Success("Successfully File Added.");
            }
            else
            {
                _notyfService.Error("Failed !!");
            }
            return RedirectToAction("ViewDocument", "Admin");
        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Case/ConcludeCare")]
        public async Task<IActionResult> ConcludeCare(ViewDocument model)
        {
            String firstname = HttpContext.Session.GetString("firstName");
            String lastName = HttpContext.Session.GetString("lastName");
            int requestId = HttpContext.Session.GetInt32("requestId").Value;
            if (await _viewDocumentsServices.uploadFile(model, firstname, lastName, requestId))
            {
                _notyfService.Success("Successfully File Added.");
            }
            else
            {
                _notyfService.Error("Failed !!");
            }
            return RedirectToAction("ConcludeCare", "Admin");
        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Order/SendOrder")]
        public async Task<IActionResult> SendOrder(SendOrder model)
        {
            if (ModelState.IsValid)
            {
                int requestId = HttpContext.Session.GetInt32("requestId").Value;
                if (await _sendOrderService.addOrderDetails(model, requestId))
                {
                    _notyfService.Success("Successfully Order Added");
                }
                else
                {
                    _notyfService.Error("Order Faild !!");
                }
                string role = HttpContext.Session.GetString("role");
                return RedirectToAction("Dashboard", role);
            }
            return View(model);
        }

        [HttpPost("/Encounter/EncounterForm")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EncounterForm(EncounterForm model)
        {
            if (ModelState.IsValid)
            {
                int requestId = HttpContext.Session.GetInt32("requestId").Value;
                int aspNetUserId = HttpContext.Session.GetInt32("aspNetUserId").Value;
                if (await _adminDashboardService.updateEncounter(model, requestId, aspNetUserId))
                {
                    _notyfService.Success("Successfully Updated");
                }
                else
                {
                    _notyfService.Error("Update Faild !!");
                }
                return RedirectToAction("Dashboard", "Admin");
            }
            _notyfService.Warning("Add Required Field.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  //  login for physician and admin both
        public IActionResult LoginPage(Login model)
        {
            if (ModelState.IsValid)
            {
                UserDataModel user = _loginService.auth(model, new List<int> { 2 , 3});
                if (!user.IsValid)
                {
                    _notyfService.Error(user.Message);
                    return View(null);
                }
                else
                {
                    HttpContext.Session.SetInt32("aspNetUserId", user.AspNetUserId);
                    HttpContext.Session.SetString("role", user.UserType);
                    HttpContext.Session.SetString("firstName", user.FirstName);
                    HttpContext.Session.SetString("lastName", user.LastName);
                    string token = _jwtService.genrateJwtToken(user);
                    CookieOptions cookieOptions = new CookieOptions()
                    {
                        Secure = true,
                        Expires = DateTime.Now.AddMinutes(20),
                    };
                    Response.Cookies.Append("jwtToken", token, cookieOptions);
                    //HttpContext.Session.SetString("jwtToken", token);
                    _notyfService.Success(user.Message);
                    return RedirectToAction("Dashboard", user.UserType);
                }
            }
            return View(null);
        }

        [HttpPost("/Case/ViewCase")]
        public async Task<IActionResult> ViewCase(ViewCase model)
        {
            if (await _adminDashboardService.updateRequest(model))
            {
                _notyfService.Success("Successfully Updated");
            }
            else
            {
                _notyfService.Error("Update Faild");
            }
            return RedirectToAction("ViewCase", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> ContactProvider(ContactProvider model)
        {
            if (await _providersService.contactProvider(model))
            {
                _notyfService.Success("Successfully Message Send");
            }
            else
            {
                _notyfService.Error("Message Send Faild");
            }
            return RedirectToAction("Providers", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> CloseCase(CloseCase model)
        {
            int requestId = HttpContext.Session.GetInt32("requestId").Value;
            if (await _closeCaseService.updateDetails(model, requestId))
            {
                _notyfService.Success("Successfully Updated");
            }
            else
            {
                _notyfService.Error("Update Faild");
            }
            return RedirectToAction("CloseCase", "Admin");
        }

        public async Task<IActionResult> RequestAddToCloseCase()    ///  from close case page button click
        {
            int requestId = HttpContext.Session.GetInt32("requestId").Value;
            if (await _closeCaseService.requestAddToCloseCase(requestId))
            {
                _notyfService.Success("Successfully Closed");
            }
            else
            {
                _notyfService.Error("Faild");
            }
            return RedirectToAction("Dashboard", "Admin");
        }

        [HttpGet]   // reset password through view profile
        public async Task<IActionResult> ViewProfileEditPassword(String newPassword)
        {
            int aspNetUserId = HttpContext.Session.GetInt32("aspNetUserId").Value;
            if (await _loginService.changePassword(aspNetUserId, newPassword))
            {
                _notyfService.Success("Successfully Updated");
            }
            else
            {
                _notyfService.Error("Faild");
            }
            return Json(new { redirect = Url.Action("ViewProfile", "Admin") });
        }

        [HttpGet]   //  Edit Administrator Information view profile
        public async Task<IActionResult> EditAdministratorInformation(String data1)
        {
            int aspNetUserId = HttpContext.Session.GetInt32("aspNetUserId").Value;
            if (await _viewProfileService.editEditAdministratorInformastion(data1, aspNetUserId))
            {
                _notyfService.Success("Successfully Updated");
            }
            else
            {
                _notyfService.Error("Faild");
            }
            return Json(new { redirect = Url.Action("ViewProfile", "Admin") });
        }

        [HttpGet]   //  Edit Mailing And Billing Information view profile
        public async Task<IActionResult> EditMailingAndBillingInformation(String data)
        {
            int aspNetUserId = HttpContext.Session.GetInt32("aspNetUserId").Value;
            if (await _viewProfileService.editMailingAndBillingInformation(data, aspNetUserId))
            {
                _notyfService.Success("Successfully Updated");
            }
            else
            {
                _notyfService.Error("Faild");
            }
            return Json(new { redirect = Url.Action("ViewProfile", "Admin") });
        }

        [HttpGet]   //  Edit Mailing And Billing Information view profile
        public async Task<IActionResult> EditProviderNotification(int physicanId, bool isNotification)
        {
            return Json(new { result = await _providersService.editProviderNotification(physicanId, isNotification) });
        }

        [HttpGet]   // Export All Data 
        public IActionResult ExportAllData()
        {
            DataTable dataTable = _adminDashboardService.exportAllData();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AllData.xlsx");
                }
            }
        }

        [HttpGet]   // Export selected Data 
        public IActionResult ExportData(String status, int pageNo, String partialViewName, String patinetName, int regionId, int requesterTypeId)
        {
            DataTable dataTable = _adminDashboardService.exportData(status, pageNo, patinetName, regionId, requesterTypeId);
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Data.xlsx");
                }
            }
        }

        [HttpGet]   // Dashboard 
        public IActionResult GetTablesData(String status, int pageNo, String partialViewName, String patinetName, int regionId, int requesterTypeId)
        {
            TableModel tableModel = _adminDashboardService.GetNewRequest(status, pageNo, patinetName, regionId, requesterTypeId);
            return tableModel.TableDatas.Count != 0 ? PartialView(partialViewName, tableModel) : PartialView("_NoTableDataFound");
        }

        [HttpPost]    // Add Business - BusinessProfile page
        public async Task<IActionResult> CreateBusiness(BusinessProfile model)
        {
            if (await _partnersService.createBusiness(model))
            {
                _notyfService.Success("Successfully Business Added");
            }
            else
            {
                _notyfService.Error("Add Business Faild");
            }
            return RedirectToAction("Partners", "Admin");
        }

        [HttpPost]    // update Business - BusinessProfile page
        public async Task<IActionResult> UpdateBusiness(BusinessProfile model)
        {
            int venderId = HttpContext.Session.GetInt32("venderId").Value;
            if (await _partnersService.EditBusiness(model,venderId))
            {
                _notyfService.Success("Successfully Business Updated");
                HttpContext.Session.Remove("venderId");
            }
            else
            {
                _notyfService.Error("Update Business Faild");
            }
            return RedirectToAction("Partners", "Admin");
        }
   
        public async Task<IActionResult> DeleteBusiness(int venderId)   // Delete Business - BusinessProfile page
        {
            if (await _partnersService.deleteBusiness(venderId))
            {
                _notyfService.Success("Successfully Business Deleted");
            }
            else
            {
                _notyfService.Error("Delete Business Faild");
            }
            return RedirectToAction("Partners", "Admin");
        }

        [HttpPost("/Case/ViewNotes")]
        public async Task<IActionResult> ViewNotes(ViewNotes model)
        {
            if (ModelState.IsValid)
            {
                int requestId = HttpContext.Session.GetInt32("requestId").Value;
                int aspNetUserId = HttpContext.Session.GetInt32("aspNetUserId").Value;
                bool isAdmin = HttpContext.Session.GetString("role") == "Admin";
                if (await _viewNotesService.addAdminNotes(model.NewNotes, requestId, aspNetUserId, isAdmin))
                {
                    _notyfService.Success("Successfully Notes Added");
                }
                else
                {
                    _notyfService.Error("Add Notes Faild");
                }
            }
            return RedirectToAction("ViewNotes", "Admin");
        }

        [HttpGet]    // Send Order
        public HealthProfessional GetBussinessData(int venderId)
        {
            return _sendOrderService.getBussinessData(venderId);
        }

        [HttpGet]    // provider scheduling page 
        public IActionResult ChangeTab(string name, int regionId, int type, String time)
        {
            switch(type)
            {
                default:
                case 1:     ///  for case 1 and 2 function is same
                case 2: return PartialView(name, _providersService.getSchedulingTableDate(regionId, type, time));
                case 3: return PartialView(name, _providersService.monthWiseScheduling(regionId,time));
            }
        }

        [HttpGet]    // RequestShift page  
        public IActionResult GetRequestShifTableData(int regionId, bool isMonth, int pageNo)
        {
            return PartialView("_RequestedShiftTable", _providersService.getRequestShiftTableDate(regionId, isMonth, pageNo));
        }

        [HttpGet]    // RequestShift page  
        public async Task<IActionResult> UpdateShiftDetails(string data, bool isApprove)
        {
            if (await _providersService.changeShiftDetails(data, isApprove))
            {
                if (isApprove)
                {
                    _notyfService.Success("Successfully Approved");
                }
                else
                {
                    _notyfService.Success("Successfully Deleted");
                }
            }
            else
            {
                _notyfService.Error("Faild!");
            }
            return Json(new { redirect = Url.Action("RequestedShift", "Admin") });
        }

        [HttpGet]       ///  provider Location page 
        public IActionResult GetProviderLocation()  
        {
            return Json(_providersService.getProviderLocation());
        }

        [HttpGet]        ///   Provider Scheduling page
        public IActionResult GetShiftDetails(int shiftDetailsId)
        {
            return Json(_providersService.getShiftDetails(shiftDetailsId));
        }

        [HttpGet]     // Edit Shift - provider Scheduling page
        public async Task<JsonResult> EditShiftDetails(string data)
        {
            int aspNetUserId = HttpContext.Session.GetInt32("aspNetUserId").Value;
            await _providersService.EditShiftDetails(data, aspNetUserId);
            return Json(new { redirect = Url.Action("ProviderScheduling", "Admin") });
        }

        [HttpGet]     // Delete Shift - provider Scheduling page
        public async Task<JsonResult> DeleteShiftDetails(string data)
        {
            await _providersService.changeShiftDetails(data, false);    ///  use same services from requested shift page for delete shift
            return Json(new { redirect = Url.Action("ProviderScheduling", "Admin") });
        }

        [HttpGet]    // Partners page 
        public IActionResult GetPatnersData(int regionId, string searchElement)
        {
            return PartialView("_PartnersTable", _partnersService.getPartnersTableDatas(regionId, searchElement));
        }

        [HttpPost]    // Record page filters
        public IActionResult GetRecordsTableDate(Records model)
        {
            return PartialView("_RecordTable", _recordService.getRecords(model).RecordTableDatas);
        }

        [HttpGet]   // Export All Data  -- Record Page
        public IActionResult ExportAllRecords() 
        {
            DataTable dataTable = _recordService.exportAllRecords();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AllData.xlsx");
                }
            }
        }

        [HttpPost]    // Email Logs page filters
        public IActionResult GetEmailLogsTableDate(EmailSmsLogs model)
        {
            return PartialView("_EmailLogTable", _recordService.getEmailLogTabledata(model));
        }

        [HttpPost]    // SMS Logs page filters
        public IActionResult GetSMSLogsTableDate(EmailSmsLogs model)
        {
            return PartialView("_SmsLogTable", _recordService.getSMSLogTabledata(model));
        }
        
        [HttpPost]    // Patient History filters
        public IActionResult GetPatinetHistoryTableDate(string model,int pageNo)
        {
            return PartialView("_PatientHistoryTable", _recordService.getPatientHistoryTable(model,pageNo));
        }

        [HttpGet]    // Provider On call Filter
        public IActionResult GetProviderOnCall(int regionId)
        {
            return PartialView("_ProviderOnCallTable", _providersService.getProviderList(regionId));
        }

        [HttpGet]    // Provider Reacord page 
        public IActionResult GetPatientRecord(int pageNo)
        {
            int userId = HttpContext.Session.GetInt32("userId").Value;
            return PartialView("_PatientRecordTable", _recordService.getPatientRecord(userId,pageNo));
        }

        [HttpPost]    // Block History filters
        public IActionResult GetBlockHistoryTableDate(string model, int pageNo, string date)
        {
            return PartialView("_BlockHistoryTable", _recordService.getBlockHistoryTable(model, pageNo, date));
        }

        [HttpPost]    // Block History page - unblock Request
        public async Task<JsonResult> UnblockRequest(int requestId)
        {
            if (await _recordService.ubblockRequest(requestId))
            {
                _notyfService.Success("Successfully UnBlocked");
            }
            else
            {
                _notyfService.Error("UnBlock Faild");
            }
            return Json(new { redirect = Url.Action("BlockHistory", "Admin") });
        }

        [HttpPost]    //  edit provier account informaction
        public async Task<IActionResult> EditphysicianAccountInformaction(EditProvider model)
        {
            int physicianId = HttpContext.Session.GetInt32("physicianId").Value;
            int aspNetUserId = HttpContext.Session.GetInt32("aspNetUserId").Value;
            if (await _providersService.editphysicianAccountInformaction(model,physicianId, aspNetUserId))
            {
                _notyfService.Success("Successfully Updated");
            }
            else
            {
                _notyfService.Error("Faild");
            }
            return RedirectToAction("EditProvider", "Admin");
        }

        [HttpPost]    //  edit provier Physician Informaction
        public async Task<IActionResult> EditphysicianPhysicianInformaction(EditProvider model)
        {
            int physicianId = HttpContext.Session.GetInt32("physicianId").Value;
            int aspNetUserId = HttpContext.Session.GetInt32("aspNetUserId").Value;
            if (await _providersService.editphysicianPhysicianInformaction(model, physicianId, aspNetUserId))
            {
                _notyfService.Success("Successfully Updated");
            }
            else
            {
                _notyfService.Error("Faild");
            }
            return RedirectToAction("EditProvider", "Admin");
        }

        [HttpPost]    //  edit provier Mailing & Billing Information
        public async Task<IActionResult> EditphysicianMailAndBillingInformaction(EditProvider model)
        {
            int physicianId = HttpContext.Session.GetInt32("physicianId").Value;
            int aspNetUserId = HttpContext.Session.GetInt32("aspNetUserId").Value;
            if (await _providersService.editphysicianMailAndBillingInformaction(model, physicianId, aspNetUserId))
            {
                _notyfService.Success("Successfully Updated");
            }
            else
            {
                _notyfService.Error("Faild");
            }
            return RedirectToAction("EditProvider", "Admin");
        }

        [HttpPost]    //  edit provier Provider Profile
        public async Task<IActionResult> EditphysicianProviderProfile(EditProvider model)
        {
            int physicianId = HttpContext.Session.GetInt32("physicianId").Value;
            int aspNetUserId = HttpContext.Session.GetInt32("aspNetUserId").Value;
            if (await _providersService.editphysicianProviderProfile(model, physicianId, aspNetUserId))
            {
                _notyfService.Success("Successfully Updated");
            }
            else
            {
                _notyfService.Error("Faild");
            }
            return RedirectToAction("EditProvider", "Admin");
        }

        [HttpPost]    //  edit provier Onbording informaction
        public async Task<IActionResult> EditphysicianOnbordingInformaction(EditProvider model)
        {
            int physicianId = HttpContext.Session.GetInt32("physicianId").Value;
            int aspNetUserId = HttpContext.Session.GetInt32("aspNetUserId").Value;
            if (await _providersService.editphysicianOnbordingInformaction(model, physicianId, aspNetUserId))
            {
                _notyfService.Success("Successfully Updated");
            }
            else
            {
                _notyfService.Error("Faild");
            }
            return RedirectToAction("EditProvider", "Admin");
        }

        [HttpPost]    //  save Signature form edit Provider
        public async Task<JsonResult> SaveSignature(string file)
        {
            int physicianId = HttpContext.Session.GetInt32("physicianId").Value;
            if (await _providersService.SaveSign(file, physicianId))
            {
                _notyfService.Success("Successfully Updated");
            }
            else
            {
                _notyfService.Error("Faild");
            }
            return Json(new { redirect = Url.Action("EditProvider", "Admin")});
        }
    }
}
