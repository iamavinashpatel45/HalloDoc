using Microsoft.AspNetCore.Http;
using Repositories.DataModels;
using Repositories.Interface;
using Repositories.Interfaces;
using Services.Interfaces.AdminServices;
using Services.Interfaces.AuthServices;
using Services.ViewModels;
using Services.ViewModels.Admin;
using Services.ViewModels.Physician;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Services.Implementation.AdminServices
{
    public class AdminDashboardService : IAdminDashboardService
    {
        private readonly IRequestClientRepository _requestClientRepository;
        private readonly IRequestRepository _requestRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IAspRepository _aspRepository;
        private readonly IEncounterRepository _encounterRepository;
        private readonly IRequestWiseFileRepository _requestWiseFileRepository;

        private Dictionary<string, List<int>> statusList { get; set; } = new Dictionary<string, List<int>>()
            {
                {"new", new List<int> { 1 } },
                {"pending", new List<int> { 2 } },
                {"active", new List<int> { 4, 5 } },
                {"conclude", new List<int> { 6 } },
                {"close", new List<int> { 3, 7, 8 } },
                {"unpaid", new List<int> { 9 } },
            };

        public AdminDashboardService(IRequestClientRepository requestClientRepository, IUserRepository userRepository, IJwtService jwtService,
                                          IAspRepository aspRepository, IRequestRepository requestRepository, IEncounterRepository encounterRepository,
                                          IRequestWiseFileRepository requestWiseFileRepository)
        {
            _requestClientRepository = requestClientRepository;
            _userRepository = userRepository;
            _jwtService = jwtService;
            _aspRepository = aspRepository;
            _requestRepository = requestRepository;
            _encounterRepository = encounterRepository;
            _requestWiseFileRepository = requestWiseFileRepository;
        }

        public AdminDashboard getallRequests()
        {
            CancelPopUp cancelPopUp = new()
            {
                Reasons = _requestClientRepository.getAllReason().ToDictionary(caseTag => caseTag.CaseTagId, caseTag => caseTag.Reason),
            };
            AssignAndTransferPopUp assignAndTransferPopUp = new()
            {
                Regions = _requestClientRepository.getAllRegions().ToDictionary(region => region.RegionId, region => region.Name),
            };
            return new AdminDashboard()
            {
                NewRequests = GetNewRequest(status: "new", pageNo: 1, patientName: "" , regionId: 0, requesterTypeId: 0),
                NewRequestCount = _requestClientRepository.countRequestClientByStatusForAdmin(statusList["new"]),
                PendingRequestCount = _requestClientRepository.countRequestClientByStatusForAdmin(statusList["pending"]),
                ActiveRequestCount = _requestClientRepository.countRequestClientByStatusForAdmin(statusList["active"]),
                ConcludeRequestCount = _requestClientRepository.countRequestClientByStatusForAdmin(statusList["conclude"]),
                TocloseRequestCount = _requestClientRepository.countRequestClientByStatusForAdmin(statusList["close"]),
                UnpaidRequestCount = _requestClientRepository.countRequestClientByStatusForAdmin(statusList["unpaid"]),
                CancelPopup = cancelPopUp,
                AssignAndTransferPopup = assignAndTransferPopUp,
            };
        }

        public TableModel GetNewRequest(String status, int pageNo, String patientName,int regionId, int requesterTypeId)
        {
            int skip = (pageNo - 1) * 10;
            Func<RequestClient, bool> predicate = a =>
            (requesterTypeId == 0 || a.Request.RequestTypeId == requesterTypeId)
            && (regionId == 0 || a.RegionId == regionId)
            && (!statusList[status].Contains(1) || a.Physician == null)
            && (patientName == null || a.FirstName.ToLower().Contains(patientName) || a.LastName.ToLower().Contains(patientName))
            && (statusList[status].Contains(a.Status));
            int totalRequests = _requestClientRepository.countRequestClientByStatusAndFilter(predicate);
            List<RequestClient> requestClients = _requestClientRepository.getRequestClientByStatus(predicate, skip: skip);
            return getTableModal(requestClients, totalRequests, pageNo);
        }

        public Dictionary<int, String> getPhysiciansByRegion(int regionId)
        {
            return _userRepository.getAllPhysicianRegionsByRegionId(regionId)
                                  .ToDictionary(phyRegion => phyRegion.PhysicianId, 
                                                phyRegion => phyRegion.Physician.FirstName + " " + phyRegion.Physician.LastName);
        }

        public Tuple<String, String, int> getRequestClientEmailAndMobile(int requestId)
        {
            RequestClient requestClient = _requestClientRepository.getRequestClientAndRequestByRequestId(requestId);
            return new Tuple<string, string, int>(requestClient.Email, requestClient.PhoneNumber, requestClient.Request.RequestTypeId);
        }

        public Agreement getUserDetails(String token)
        {
            Agreement agreement = new Agreement();
            try
            {
                JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(token);
                if (_jwtService.validateToken(token, out jwtSecurityToken))
                {
                    int requestId = int.Parse(jwtSecurityToken.Claims.FirstOrDefault(a => a.Type == "requestId").Value);
                    RequestClient requestClient = _requestClientRepository.getRequestClientByRequestId(requestId);
                    if(requestClient.Status == 5)
                    {
                        agreement = new Agreement()
                        {
                            IsValid = true,
                            FirstName = requestClient.FirstName,
                            LastName = requestClient.LastName,
                            RequestId = requestId,
                        };
                        return agreement;
                    }
                    else
                    {
                        agreement = new Agreement()
                        {
                            IsValid = false,
                            Message = "Agreement Already Agreed",
                        };
                        return agreement;
                    }
                }
            }
            catch (Exception ex) { }
            agreement = new Agreement()
            {
                IsValid = false,
                Message = "Link Is Not Valid",
            };
            return agreement;
        }

        public bool SendRequestLink(SendLink model,HttpContext httpContext)
        {
            var request = httpContext.Request;
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("tatva.dotnet.avinashpatel@outlook.com"),
                Subject = "Link For Patient Request",
                IsBodyHtml = true,
                Body = "Link For Patient Request: "+request.Scheme+"://"+request.Host+"/Patient/PatientRequest",
            };
            //mailMessage.To.Add(model.Email);
            mailMessage.To.Add("tatva.dotnet.avinashpatel@outlook.com");
            SmtpClient smtpClient = new SmtpClient("smtp.office365.com")
            {
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
                Port = 587,
                Credentials = new NetworkCredential(userName: "tatva.dotnet.avinashpatel@outlook.com", password: "Avinash@6351"),
            };
            try
            {
                smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> createRequest(CreateRequest model, int aspNetUserIdUser, bool isAdmin)
        {
            int aspNetUserId = _aspRepository.checkUser(email: model.Email);
            int userId = _userRepository.getUserID(aspNetUserId);
            if (aspNetUserId == 0)
            {
                AspNetUser aspNetUser = new()
                {
                    UserName = model.FirstName,
                    Email = model.Email,
                    PhoneNumber = model.Mobile,
                    PasswordHash = genrateHash(model.Password),
                    CreatedDate = DateTime.Now,
                };
                aspNetUserId = await _aspRepository.addUser(aspNetUser);
                User user = new()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Mobile = model.Mobile,
                    Street = model.Street,
                    City = model.City,
                    State = model.State,
                    ZipCode = model.ZipCode,
                    AspNetUserId = aspNetUserId,
                    CreatedBy = aspNetUserId,
                    CreatedDate = DateTime.Now,
                    IntYear = model.BirthDate.Value.Year,
                    IntDate = model.BirthDate.Value.Day,
                    StrMonth = model.BirthDate.Value.Month.ToString(),
                };
                userId = await _userRepository.addUser(user);
                AspNetUserRole aspNetUserRole = new()
                {
                    UserId = aspNetUserId,
                    RoleId = _aspRepository.checkUserRole(role: "Patient"),
                };
                await _aspRepository.addAspNetUserRole(aspNetUserRole);
            }
            Request request = new Request();
            int physicianId = 0;
            if(isAdmin)
            {
                Admin admin = _userRepository.getAdmionByAspNetUserId(aspNetUserIdUser);
                request = new Request()
                {
                    RequestTypeId = 5,
                    FirstName = admin.FirstName,
                    LastName = admin.LastName,
                    Email = admin.Email,
                    PhoneNumber = admin.Mobile,
                    UserId = userId,
                    CreatedDate = DateTime.Now,
                };
            }
            else
            {
                Physician physician = _userRepository.getPhysicianByAspNetUserId(aspNetUserIdUser);
                request = new Request()
                {
                    RequestTypeId = 5,
                    FirstName = physician.FirstName,
                    LastName = physician.LastName,
                    Email = physician.Email,
                    PhoneNumber = physician.Mobile,
                    UserId = userId,
                    CreatedDate = DateTime.Now,
                };
                physicianId = physician.PhysicianId;
            }
            int requestId = await _requestRepository.addRequest(request);
            RequestClient requestClient = new()
            {
                RequestId = requestId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.Mobile,
                Email = model.Email,
                State = model.State,
                Street = model.Street,
                City = model.City,
                ZipCode = model.ZipCode,
                Status = isAdmin? 1 : 2,
                Symptoms = model.Symptoms,
                IntYear = DateTime.Now.Year,
                IntDate = DateTime.Now.Day,
                StrMonth = DateTime.Now.Month.ToString(),
                PhysicianId = isAdmin ? null : physicianId,
            };
            return await _requestClientRepository.addRequestClient(requestClient);
        }

        public bool RequestSupport(RequestSupport model)
        {
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("tatva.dotnet.avinashpatel@outlook.com"),
                Subject = "Request DTY Support",
                IsBodyHtml = true,
                Body = "We are short on coverage and needs additional support On Call to respond to Requests. And Admin Message :: " + model.Message
            };
            SmtpClient smtpClient = new SmtpClient("smtp.office365.com")
            {
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
                Port = 587,
                Credentials = new NetworkCredential(userName: "tatva.dotnet.avinashpatel@outlook.com", password: "Avinash@6351"),
            };
            //List<Physician> physicians = _userRepository.getAllUnAssignedPhysician();
            //foreach (Physician physician in physicians)
            //{
            //    mailMessage.Bcc.Add(physician.Email);
            //}
            mailMessage.To.Add("tatva.dotnet.avinashpatel@outlook.com");
            try
            {
                smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public ConcludeCare getConcludeCare(int requestId)
        {
            RequestClient requestClient = _requestClientRepository.getRequestClientByRequestId(requestId);
            return new ConcludeCare()
            {
                FirstName = requestClient.FirstName,
                LastName = requestClient.LastName,
                FileList = _requestWiseFileRepository.getFilesByrequestId(requestId)
                            .Select(requestWiseFile =>
                            new FileModel()
                            {
                                RequestId = requestId,
                                RequestWiseFileId = requestWiseFile.RequestWiseFileId,
                                FileName = requestWiseFile.FileName,
                                Uploder = requestWiseFile.Uploder,
                                CreatedDate = requestWiseFile.CreatedDate,
                            }).ToList(),
            };
        }

        public EncounterForm getEncounterDetails(int requestId, bool isAdmin)
        {
            Encounter encounter = _encounterRepository.getEncounter(requestId);
            if (encounter != null)
            {
                return new EncounterForm()
                {
                    IsAdmin = isAdmin,
                    FirstName = encounter.FirstName,
                    LastName = encounter.LastName,
                    Location = encounter.Location,
                    Email = encounter.Email,
                    BirthDate = DateTime.Parse(encounter.IntYear + "-" + encounter.StrMonth + "-" + encounter.IntDate),
                    Date = encounter.Date,
                    Mobile = encounter.PhoneNumber,
                    HistoryOfIllness = encounter.IllnessOrInjury,
                    MedicalHistory = encounter.MedicalHistory,
                    Medications = encounter.Medications,
                    Allergies = encounter.Allergies,
                    Temp = encounter.Temperature,
                    HeartRate = encounter.HeartRate,
                    RespiratoryRate = encounter.RespiratoryRate,
                    BloodPressure1 = encounter.BloodPressure1,
                    BloodPressure2 = encounter.BloodPressure2,
                    O2 = encounter.O2,
                    Pain = encounter.Pain,
                    Heent = encounter.Heent,
                    CV = encounter.Cv,
                    Chest = encounter.Chest,
                    ABD = encounter.Abd,
                    Extra = encounter.Extr,
                    Skin = encounter.Skin,
                    Neuro = encounter.Neuro,
                    Other = encounter.Other,
                    Diagnosis = encounter.Diagnosis,
                    TreatmentPlan = encounter.TreatmentPlan,
                    Dispensed = encounter.MedicationsDispensed,
                    Procedures = encounter.Procedures,
                    FollowUp = encounter.Followup,
                };
            }
            else
            {
                RequestClient requestClient = _requestClientRepository.getRequestClientByRequestId(requestId);  
                return new EncounterForm() 
                { 
                    IsAdmin = isAdmin,
                    FirstName = requestClient.FirstName,
                    LastName = requestClient.LastName,
                    Email = requestClient.Email,
                    Mobile = requestClient.PhoneNumber,
                    Location = $"{requestClient.Street}, {requestClient.City}, {requestClient.State}, {requestClient.ZipCode}",
                    BirthDate = requestClient.IntYear != null ? DateTime.Parse(requestClient.IntYear + "-" + requestClient.StrMonth
                                                                   + "-" + requestClient.IntDate) : null,
                    Date = DateTime.Now,
                };
            }
        }

        public async Task<bool> updateEncounter(EncounterForm model, int requestId, int aspNetUserId)
        {
            Encounter encounter = _encounterRepository.getEncounter(requestId);
            if (encounter == null)
            {
                encounter = new Encounter();
                encounter = setEncounterProperties(encounter, model, aspNetUserId, requestId);
                return await _encounterRepository.addEncounter(encounter);
            }
            else
            {
                encounter = setEncounterProperties(encounter, model, aspNetUserId, requestId);
                encounter.ModifiedDate = DateTime.Now;
                encounter.ModifiedBy = aspNetUserId.ToString();
                return await _encounterRepository.updateEncounter(encounter);
            }
        }

        private Encounter setEncounterProperties(Encounter encounter, EncounterForm model, int aspNetUserId, int requestId)
        {
            encounter.IsFinalize = model.IsFinalize;
            encounter.FinalizeBy = model.IsFinalize ? aspNetUserId : null;
            encounter.RequestId = requestId;
            encounter.FirstName = model.FirstName;
            encounter.LastName = model.LastName;
            encounter.Location = model.Location;
            encounter.Email = model.Email;
            encounter.IntYear = model.BirthDate.Value.Year;
            encounter.IntDate = model.BirthDate.Value.Day;
            encounter.StrMonth = model.BirthDate.Value.Month.ToString();
            encounter.Date = model.Date;
            encounter.PhoneNumber = model.Mobile;
            encounter.IllnessOrInjury = model.HistoryOfIllness;
            encounter.MedicalHistory = model.MedicalHistory;
            encounter.Medications = model.Medications;
            encounter.Allergies = model.Allergies;
            encounter.Temperature = model.Temp;
            encounter.HeartRate = model.HeartRate;
            encounter.RespiratoryRate = model.RespiratoryRate;
            encounter.BloodPressure1 = model.BloodPressure2;
            encounter.BloodPressure2 = model.BloodPressure2;
            encounter.O2 = model.O2;
            encounter.Pain = model.Pain;
            encounter.Heent = model.Heent;
            encounter.Cv = model.CV;
            encounter.Chest = model.Chest;
            encounter.Abd = model.ABD;
            encounter.Extr = model.Extra;
            encounter.Skin = model.Skin;
            encounter.Neuro = model.Neuro;
            encounter.Other = model.Other;
            encounter.Diagnosis = model.Diagnosis;
            encounter.TreatmentPlan = model.TreatmentPlan;
            encounter.MedicationsDispensed = model.Dispensed;
            encounter.Procedures = model.Procedures;
            encounter.Followup = model.FollowUp;
            return encounter;
        }

        public ViewCase getRequestDetails(int requestId, bool isAdmin)
        {
            RequestClient requestClient = _requestClientRepository.getRequestClientAndRequestByRequestId(requestId);
            return new ViewCase()
            {
                IsAdmin = isAdmin,
                Status = requestClient.Status,
                Requester = requestClient.Request.RequestTypeId,
                RequestId = requestId,
                PatientNotes = requestClient.Symptoms,
                FirstName = requestClient.FirstName,
                LastName = requestClient.LastName,
                BirthDate = requestClient.IntYear != null ? DateTime.Parse(requestClient.IntYear + "-" + requestClient.StrMonth
                                 + "-" + requestClient.IntDate) : null,
                Mobile = requestClient.PhoneNumber,
                Email = requestClient.Email,
                Address = requestClient.Street + " " + requestClient.City + " " + requestClient.State + " " + requestClient.ZipCode,
                Region = requestClient.State,
                CancelPopup = new CancelPopUp(){
                                Reasons = _requestClientRepository.getAllReason().ToDictionary(caseTag => caseTag.CaseTagId, caseTag => caseTag.Reason),
                              },
            };
        }

        public async Task<bool> updateRequest(ViewCase model)
        {
            RequestClient requestClient = _requestClientRepository.getRequestClientByRequestId(model.RequestId);
            requestClient.Symptoms = model.PatientNotes;
            requestClient.FirstName = model.FirstName;
            requestClient.LastName = model.LastName;
            requestClient.PhoneNumber = model.Mobile;
            requestClient.IntYear = model.BirthDate.Value.Year;
            requestClient.IntDate = model.BirthDate.Value.Day;
            requestClient.StrMonth = model.BirthDate.Value.Month.ToString();
            return await _requestClientRepository.updateRequestClient(requestClient);
        }

        public DataTable exportData(String status, int pageNo, String patientName, int regionId, int requesterTypeId)
        {
            return convertRequestClientToDataTable(GetNewRequest(status, pageNo, patientName, regionId, requesterTypeId).TableDatas);
        }

        public DataTable exportAllData()
        {
            return convertRequestClientToDataTable(_requestClientRepository.getAllRequestClients());
        }

        private TableModel getTableModal(List<RequestClient> requestClients, int totalRequests, int pageNo)
        {
            int skip = (pageNo - 1) * 10;
            List<TablesData> tablesDatas = requestClients
                .Select(requestClient => new TablesData()
                {
                    RequestId = requestClient.RequestId,
                    FirstName = requestClient.FirstName,
                    LastName = requestClient.LastName,
                    Requester = requestClient.Request.RequestTypeId,
                    RequesterFirstName = requestClient.Request.FirstName,
                    RequesterLastName = requestClient.Request.LastName,
                    Mobile = requestClient.PhoneNumber,
                    RequesterMobile = requestClient.Request.PhoneNumber,
                    State = requestClient.State,
                    Street = requestClient.Street,
                    ZipCode = requestClient.ZipCode,
                    City = requestClient.City,
                    Notes = requestClient.Symptoms,
                    RegionId = requestClient.RegionId,
                    PhysicianName = requestClient.Physician != null ? requestClient.Physician.FirstName + " " + requestClient.Physician.LastName : "",
                    RequesterType = requestClient.Request.RequestTypeId,
                    BirthDate = requestClient.IntYear != null ? DateTime.Parse(requestClient.IntYear + "-" + requestClient.StrMonth
                                                                   + "-" + requestClient.IntDate).ToString("MMM dd, yyyy") : "",
                    RequestdDate = requestClient.Request.CreatedDate.ToString("MMM dd, yyyy"),
                    Email = requestClient.Email,
                    DateOfService = null,
                }).ToList();
            int totalPages = totalRequests % 10 != 0 ? (totalRequests / 10) + 1 : totalRequests / 10;
            return new TableModel()
            {
                IsFirstPage = pageNo != 1,
                IsLastPage = pageNo != totalPages,
                IsNextPage = pageNo < totalPages,
                IsPreviousPage = pageNo > 1,
                TableDatas = tablesDatas,
                TotalRequests = totalRequests,
                PageNo = pageNo,
                StartRange = skip + 1,
                EndRange = skip + 10 < totalRequests ? skip + 10 : totalRequests,
            };
        }

        private DataTable convertRequestClientToDataTable(List<RequestClient> requestClients)
        {
            List<String> columnsNames = new List<String>();
            DataTable dataTable = new DataTable();
            dataTable.TableName = "RequestDatas";
            int currentRow = 1, index = 1;
            foreach (PropertyInfo propertyInfo in typeof(RequestClient).GetProperties())
            {
                dataTable.Columns.Add(propertyInfo.Name);
                columnsNames.Add(propertyInfo.Name);
                index++;
            }
            DataRow row;
            foreach (RequestClient requestClient in requestClients)
            {
                row = dataTable.NewRow();
                for (int i = 0; i < columnsNames.Count; i++)
                {
                    var value = typeof(RequestClient).GetProperty(columnsNames[i]).GetValue(requestClient);
                    if (value != null)
                    {
                        row[columnsNames[i]] = value.ToString();
                    }
                }
                dataTable.Rows.Add(row);
                currentRow++;
            }
            return dataTable;
        }

        private DataTable convertRequestClientToDataTable(List<TablesData> tablesDatas)
        {
            List<String> columnsNames = new List<String>();
            DataTable dataTable = new DataTable();
            dataTable.TableName = "RequestDatas";
            foreach (PropertyInfo propertyInfo in typeof(TablesData).GetProperties())
            {
                dataTable.Columns.Add(propertyInfo.Name);
                columnsNames.Add(propertyInfo.Name);
            }
            DataRow row;
            int currentRow = 1;
            foreach (TablesData tablesData in tablesDatas)
            {
                row = dataTable.NewRow();
                for (int i = 0; i < columnsNames.Count; i++)
                {
                    var value = typeof(TablesData).GetProperty(columnsNames[i]).GetValue(tablesData);
                    if (value != null)
                    {
                        row[columnsNames[i]] = value.ToString();
                    }
                }
                dataTable.Rows.Add(row);
                currentRow++;
            }
            return dataTable;
        }

        private String genrateHash(String password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashPassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashPassword).Replace("-", "").ToLower();
            }
        }
    }
}