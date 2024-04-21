using iText.Kernel.Pdf;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Repositories.DataModels;
using Repositories.Interfaces;
using Services.Interfaces.PhysicianServices;
using Services.ViewModels.Admin;
using Services.ViewModels.Physician;
using System.Collections;
using System.Reflection;

namespace Services.Implementation.PhysicianServices
{
    public class PhysicianDashboardService : IPhysicianDashboardService
    {
        private readonly IRequestClientRepository _requestClientRepository;
        private readonly IRequestStatusLogRepository _requestStatusLogRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEncounterRepository _encounterRepository;
        private readonly IShiftRepository _shiftRepository;

        private Dictionary<string, List<int>> statusList { get; set; } = new Dictionary<string, List<int>>()
            {
                {"new", new List<int> { 1 } },
                {"pending", new List<int> { 2 } },
                {"active", new List<int> { 4, 5 } },
                {"conclude", new List<int> { 6 } },
            };

        public PhysicianDashboardService(IRequestClientRepository requestClientRepository, IRequestStatusLogRepository requestStatusLogRepository,
                                                        IUserRepository userRepository, IEncounterRepository encounterRepository, 
                                                        IShiftRepository shiftRepository)
        {
            _requestClientRepository = requestClientRepository;
            _requestStatusLogRepository = requestStatusLogRepository;
            _userRepository = userRepository;
            _encounterRepository = encounterRepository;
            _shiftRepository = shiftRepository;
        }

        public async Task<bool> acceptRequest(int requestId)
        {
            RequestClient requestClient = _requestClientRepository.getRequestClientByRequestId(requestId);
            requestClient.Status = 2;
            return await _requestClientRepository.updateRequestClient(requestClient);
        }

        public async Task<bool> setEncounter(int requestId, bool isVideoCall)
        {
            RequestClient requestClient = _requestClientRepository.getRequestClientAndRequestByRequestId(requestId);
            requestClient.Status = isVideoCall ? 6   : 4;
            requestClient.Request.CallType = isVideoCall ? (short)1 : (short)2;
            return await _requestClientRepository.updateRequestClient(requestClient);
        }

        public int getPhysicianIdFromAspNetUserId(int aspNetUserId)
        {
            return _userRepository.getPhysicianByAspNetUserId(aspNetUserId).PhysicianId;
        }

        public async Task<bool> concludeCare(int requestId, ConcludeCare model)
        {
            Encounter encounter = _encounterRepository.getEncounter(requestId);
            if (encounter!=null && (bool)encounter.IsFinalize)
            {
                RequestClient requestClient = _requestClientRepository.getRequestClientAndRequestByRequestId(requestId);
                requestClient.Status = 8;
                if (await _requestClientRepository.updateRequestClient(requestClient))
                {
                    return await _requestStatusLogRepository.addRequestSatatusLog(
                        new RequestStatusLog()
                        {
                            RequestId = requestId,
                            Status = 8,
                            CreatedDate = DateTime.Now,
                            Notes = model.Notes,
                        });
                }
            }
            return false;
        }

        public byte[] generateMedicalReport(int requestId)
        {
            Encounter encounter = _encounterRepository.getEncounter(requestId);
            EncounterForm encounterForm = new EncounterForm()
            {
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
            using (var memoryStream = new System.IO.MemoryStream())
            {
                PdfWriter writer = new PdfWriter(memoryStream);
                PdfDocument pdf = new PdfDocument(writer);
                iText.Layout.Document document = new iText.Layout.Document(pdf);
                Div div = new Div();
                document.Add(new Paragraph("Medical Report"));
                document.Add(new Paragraph($"Patient Name: \t\t {encounterForm.FirstName + " " + encounterForm.LastName}"));
                div.Add(new Paragraph($"Email:\t\t {encounterForm.Email}"));
                div.Add(new Paragraph($"Mobile Number:\t\t {encounterForm.Mobile}"));
                document.Add(new Paragraph($"Date Of Birth: \t\t {encounterForm.BirthDate}"));
                document.Add(new Paragraph($"Report Date:\t\t {encounterForm.Date.ToString()}"));
                document.Add(new Paragraph($"PDF Generate Date:\t\t {DateTime.Now.ToShortDateString()}"));
                document.Add(new Paragraph($"Address:\t\t {encounterForm.Location}"));
                Table nestedTable1 = new Table(UnitValue.CreatePercentArray(new float[] { 100, 395 }));
                nestedTable1.SetMinWidth(495);
                int count = 1;
                foreach (PropertyInfo propertyInfo in typeof(EncounterForm).GetProperties())
                {
                    try
                    {
                        if(count > 9)
                        {
                            var value = typeof(EncounterForm).GetProperty(propertyInfo.Name).GetValue(encounterForm);
                            if (value != null)
                            {
                                nestedTable1.AddCell(new Cell().Add(new Paragraph($"{propertyInfo.Name}").SetBold()).SetWidth(100));
                                nestedTable1.AddCell(new Cell().Add(new Paragraph(value.ToString())));
                            }
                        }
                        count++;
                    }
                    catch { }
                }
                document.Add(nestedTable1.SetBorder(Border.NO_BORDER).SetPadding(0));
                document.Close();
                return memoryStream.ToArray();
            }
        }

        public PhysicianScheduling providerScheduling(int aspNetUserId)
        {
            CreateShift createShift = new CreateShift()
            {
                Regions = _userRepository.getAllPhysicianRegionsByAspNetUserIdWithRegionName(aspNetUserId)
                                         .ToDictionary(physicianRegion => physicianRegion.RegionId, physicianRegion => physicianRegion.Region.Name),
            };
            DateTime monthStartDate = DateTime.Now.AddDays( 1 - DateTime.Now.Day);
            return new PhysicianScheduling()
            {
                TableData = monthWiseScheduling(monthStartDate.ToString(), aspNetUserId),
                CreateShift = createShift,
            };
        }

        public SchedulingTableMonthWise monthWiseScheduling(string dateString, int aspNetUserId)
        {
            DateTime date = DateTime.Parse(dateString);
            int startDate = (int)date.DayOfWeek;
            Dictionary<int, List<ShiftDetailsMonthWise>> monthWiseScheduling = new Dictionary<int, List<ShiftDetailsMonthWise>>();
            int totalDays = DateTime.DaysInMonth(date.Year, date.Month);
            _shiftRepository.getShiftDetailByPhysicianIdAndDAte(aspNetUserId, startDate: date, endDate: date.AddMonths(1).AddDays(-1))
                .ForEach(shiftDetail =>
                {
                    int currentDay = shiftDetail.ShiftDate.Day;
                    if (!monthWiseScheduling.ContainsKey(currentDay))
                    {
                        monthWiseScheduling.Add(currentDay, new List<ShiftDetailsMonthWise>());
                    }
                    monthWiseScheduling[currentDay].Add(new ShiftDetailsMonthWise()
                    {
                        ProviderName = shiftDetail.Shift.Physician.FirstName + " " + shiftDetail.Shift.Physician.LastName,
                        ShiftDetailsId = shiftDetail.ShiftDetailId,
                        StartTime = shiftDetail.StartTime,
                        EndTime = shiftDetail.EndTime,
                        Status = shiftDetail.Status == 0 ? "bg-pink" : "bg-green",
                    });
                });
            return new SchedulingTableMonthWise()
            {
                MonthWise = monthWiseScheduling,
                StartDate = startDate,
                TotalDays = totalDays,
            };
        }

        public async Task<bool> transferRequest(PhysicianTransferRequest model)
        {
            RequestClient requestClient = _requestClientRepository.getRequestClientByRequestId(model.RequestId);
            requestClient.Status = 1;
            requestClient.PhysicianId = null;
            if(await _requestClientRepository.updateRequestClient(requestClient))
            {
                return await _requestStatusLogRepository
                    .addRequestSatatusLog(
                    new RequestStatusLog()
                    {
                        RequestId = model.RequestId,
                        Status = 1,
                        CreatedDate = DateTime.Now,
                        Notes = model.TransferNotes,
                        TransToAdmin = new BitArray(1,true),
                    });
            }
            return false;
        }

        public PhysicianDashboard getallRequests(int aspNetUserId)
        {
            CancelPopUp cancelPopUp = new()
            {
                Reasons = _requestClientRepository.getAllReason().ToDictionary(caseTag => caseTag.CaseTagId, caseTag => caseTag.Reason),
            };
            AssignAndTransferPopUp assignAndTransferPopUp = new()
            {
                Regions = _requestClientRepository.getAllRegions().ToDictionary(region => region.RegionId, region => region.Name),
            };
            return new PhysicianDashboard()
            {
                NewRequests = GetNewRequest(status: "new", pageNo: 1, patientName: "", regionId: 0, requesterTypeId: 0, aspNetUserId),
                NewRequestCount = _requestClientRepository.countRequestClientByStatusForPhysician(statusList["new"],aspNetUserId),
                PendingRequestCount = _requestClientRepository.countRequestClientByStatusForPhysician(statusList["pending"], aspNetUserId),
                ActiveRequestCount = _requestClientRepository.countRequestClientByStatusForPhysician(statusList["active"], aspNetUserId),
                ConcludeRequestCount = _requestClientRepository.countRequestClientByStatusForPhysician(statusList["conclude"], aspNetUserId),
                Regions = _requestClientRepository.getAllRegions().ToDictionary(region => region.RegionId, region => region.Name),
            };
        }

        public TableModel GetNewRequest(String status, int pageNo, String patientName, int regionId, int requesterTypeId, int aspNetUserId)
        {
            int skip = (pageNo - 1) * 10;
            Func<RequestClient, bool> predicate = a =>
            (requesterTypeId == 0 || a.Request.RequestTypeId == requesterTypeId)
            && (regionId == 0 || a.RegionId == regionId)
            && (patientName == null || a.FirstName.ToLower().Contains(patientName) || a.LastName.ToLower().Contains(patientName))
            && (!statusList[status].Contains(1) || a.Physician != null)
            && a.Physician != null
            && a.Physician.AspNetUserId == aspNetUserId
            && (statusList[status].Contains(a.Status));
            int totalRequests = _requestClientRepository.countRequestClientByStatusAndFilter(predicate);
            List<RequestClient> requestClients = _requestClientRepository.getRequestClientByStatus(predicate, skip: skip);
            return getTableModal(requestClients, totalRequests, pageNo);
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
                    RequesterType = requestClient.Request.RequestTypeId,
                    Email = requestClient.Email,
                    IsEncounter = requestClient.Request.CallType != null ? 1 : 0,
                    EncounterType = requestClient.Request.CallType,
                    Isfinalize = requestClient.Request.Encounters.FirstOrDefault(a => a.RequestId == requestClient.RequestId) != null ?
                                  (bool)requestClient.Request.Encounters.FirstOrDefault(a => a.RequestId == requestClient.RequestId).IsFinalize ? 1: 0 : 0,
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
    }
}
