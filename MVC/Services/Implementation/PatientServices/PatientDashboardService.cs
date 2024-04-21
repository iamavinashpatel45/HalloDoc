using Repositories.Interfaces;
using Services.Interfaces.PatientServices;
using Services.ViewModels;
using System.Collections;

namespace Services.Implementation.PatientServices
{
    public class PatientDashboardService : IPatientDashboardService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRequestClientRepository _requestClientRepository;

        public PatientDashboardService(IUserRepository userRepository, IRequestClientRepository requestClientRepository)
        {
            _userRepository = userRepository;
            _requestClientRepository = requestClientRepository;
        }

        public List<Dashboard> GetUsersMedicalData(int aspNetUserId)
        {
            return _requestClientRepository.getAllRequestClientForUser(_userRepository.getUserID(aspNetUserId))
                    .Select(requestClient =>
                    new Dashboard()
                    {
                        RequestId = requestClient.RequestId,
                        StrMonth = requestClient.StrMonth,
                        IntYear = requestClient.IntYear,
                        IntDate = requestClient.IntDate,
                        Status = requestClient.Status,
                        Document = requestClient.Request.RequestWiseFiles == null ? 0 : 
                                           requestClient.Request.RequestWiseFiles.Where(a => a.IsDeleted != new BitArray(1,true)).Count(),
                    }).ToList();
        }
    }
}
