using AspNetCoreHero.ToastNotification.Abstractions;
using HelloDoc.Authentication;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Interfaces.AuthServices;
using Services.Interfaces.PatientServices;
using Services.ViewModels;

namespace HelloDoc.Controllers
{
    public class PatientController : Controller
    {
        private readonly INotyfService _notyfService;
        private readonly ILoginService _loginService;
        private readonly IPatientDashboardService _dashboardService;
        private readonly IAddRequestService _addRequestService;
        private readonly IViewProfileService _viewProfileService;
        private readonly IViewDocumentsServices _viewDocumentsServices;
        private readonly IJwtService _jwtService;

        public PatientController(INotyfService notyfService,ILoginService loginService ,IPatientDashboardService dashboardService,
                                 IAddRequestService addRequestService,IViewProfileService viewProfileService, 
                                 IViewDocumentsServices viewDocumentsServices,IJwtService jwtService)
        {
            _notyfService = notyfService;
            _loginService = loginService;
            _dashboardService = dashboardService;
            _addRequestService = addRequestService;
            _viewProfileService= viewProfileService;
            _viewDocumentsServices = viewDocumentsServices;
            _jwtService = jwtService;
        }

        [Route("/")]
        public IActionResult PatientSite()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            _notyfService.Warning("Access Denied !!");
            return RedirectToAction("PatientSite", "Patient");
        }

        public IActionResult LoginPage()
        {
            string role = _loginService.isTokenValid(HttpContext,new List<int> { 1 });
            if (role != null)
            {
                return RedirectToAction("Dashboard", role);
            }
            return View();
        }

        public IActionResult SubmitRequest()
        {
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        public IActionResult PatientRequest()
        {
            return View();
        }
        public IActionResult FamilyFriendRequest()
        {
            return View();
        }

        public IActionResult ConciergeRequest()
        {
            return View();
        }

        public IActionResult BusinessRequest()
        {
            return View();
        }

        public IActionResult NewPassword(String token, int id, string time)
        {
            SetNewPassword setNewPassword = _loginService.validatePasswordLink(token);
            if (!setNewPassword.IsValidLink)
            {
                _notyfService.Error(setNewPassword.ErrorMessage);
            }
            return View(setNewPassword);
        }

        [Authorization("Patient")]
        public IActionResult RequestForSomeOne()
        {
            return View();
        }

        [Authorization("Patient")]
        public IActionResult RequestForMe()
        {
            int aspNetUserId = HttpContext.Session.GetInt32("aspNetUserId").Value;
            return View(_addRequestService.getModelForRequestByMe(aspNetUserId));
        }

        [Authorization("Patient")]
        public IActionResult ViewProfile()
        {
            int aspNetUserId = HttpContext.Session.GetInt32("aspNetUserId").Value;
            return View(_viewProfileService.getProfileDetails(aspNetUserId));
        }

        [Authorization("Patient")]
        public IActionResult ViewDocument()
        {
            int aspNetUserId = HttpContext.Session.GetInt32("aspNetUserId").Value;
            int requestId = HttpContext.Session.GetInt32("requestId").Value;
            return View(_viewDocumentsServices.getDocumentList(requestId: requestId,aspNetUserId: aspNetUserId));
        }

        [Authorization("Patient")]
        public IActionResult Dashboard(string tab)
        {
            int aspNetUserId = HttpContext.Session.GetInt32("aspNetUserId").Value;
            return View(_dashboardService.GetUsersMedicalData(aspNetUserId));
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("aspNetUserId");
            HttpContext.Session.Remove("role");
            Response.Cookies.Delete("jwtToken");
            return RedirectToAction("LoginPage", "Patient");
        }

        [HttpGet]
        public JsonResult SetRequestId(int requestId)
        {
            HttpContext.Session.SetInt32("requestId", requestId);
            return Json(new { redirect = Url.Action("ViewDocument","Patient") });
        }

        [HttpGet] 
        public JsonResult CheckEmailExists(string email)
        {
            var emailExists = _addRequestService.IsEmailExists(email);
            return Json(new { emailExists });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoginPage(Login model)
        {
            UserDataModel user = _loginService.auth(model, new List<int> { 1 });
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
                return RedirectToAction("Dashboard", "Patient");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewPassword(SetNewPassword model)
        {
            if (ModelState.IsValid)
            {
                if(await _loginService.changePassword(aspNetUserId: model.AspNetUserId,password: model.Password))
                {
                    _notyfService.Success("Successfully Password Updated");
                    return RedirectToAction("PatientSite", "Patient");
                }
            }
            return View(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ViewProfile(ViewProfile model)
        {
            int aspNetUserId = HttpContext.Session.GetInt32("aspNetUserId").Value;
            if (await _viewProfileService.updatePatientProfile(model, aspNetUserId))
            {
                _notyfService.Success("Successfully Updated");
            }
            else
            {
                _notyfService.Error("Faild!");
            }
            return RedirectToAction("ViewProfile", "Patient");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPassword model)
        {
            if(await _loginService.resetPasswordLinkSend(model.Email, HttpContext))
            {
                _notyfService.Success("Successfully Email Send");
                return RedirectToAction("PatientSite", "Patient");
            }
            _notyfService.Error("Email Not Found");
            return View(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                _notyfService.Error("File");
            }
            return RedirectToAction("viewDocument", "Patient");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestForMe(AddRequestByPatient model)
        {
            if (await _addRequestService.addRequestForMe(model))
            {
                _notyfService.Success("Successfully Request Added");
                return RedirectToAction("Dashboard", "Patient");
            }
            else
            {
                _notyfService.Error("Add Request Faild");
                return RedirectToAction("RequestForMe", "Patient");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestForSomeOne(AddRequestByPatient model)
        {
            int aspNetUserId = HttpContext.Session.GetInt32("aspNetUserId").Value;
            if (await _addRequestService.addRequestForSomeOneelse(model: model, aspNetUserIdMe: aspNetUserId))
            {
                _notyfService.Success("Successfully Request Added");
                return RedirectToAction("Dashboard", "Patient");
            }
            else
            {
                _notyfService.Error("Add Request Faild");
                return RedirectToAction("RequestForSomeOne", "Patient");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PatientRequest(AddPatientRequest model)
        {
            if (await _addRequestService.addPatientRequest(model))
            {
                _notyfService.Success("Successfully Request Added");
                return RedirectToAction("LoginPage", "Patient");
            }
            else
            {
                _notyfService.Error("Add Request Faild");
                return RedirectToAction("PatientRequest", "Patient");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConciergeRequest(AddConciergeRequest model)
        {
            if (await _addRequestService.addConciergeRequest(model))
            {
                _notyfService.Success("Successfully Request Added");
                return RedirectToAction("LoginPage", "Patient");
            }
            else
            {
                _notyfService.Error("Add Request Faild");
                return RedirectToAction("ConciergeRequest", "Patient");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FamilyFriendRequest(AddFamilyRequest model)
        {
            if (await _addRequestService.addFamilyFriendRequest(model))
            {
                _notyfService.Success("Successfully Request Added");
                return RedirectToAction("LoginPage", "Patient");
            }
            else
            {
                _notyfService.Error("Add Request Faild");
                return RedirectToAction("FamilyFriendRequest", "Patient");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BusinessRequest(AddBusinessRequest model)
        {
            if (await _addRequestService.addBusinessRequest(model))
            {
                _notyfService.Success("Successfully Request Added");
                return RedirectToAction("LoginPage", "Patient");
            }
            else
            {
                _notyfService.Error("Add Request Faild");
                return RedirectToAction("BusinessRequest", "Patient");
            }
        }
    }
}
