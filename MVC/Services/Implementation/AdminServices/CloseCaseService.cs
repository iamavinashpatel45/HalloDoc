using Repositories.DataModels;
using Repositories.Interfaces;
using Services.Interfaces.AdminServices;
using Services.ViewModels;
using Services.ViewModels.Admin;

namespace Services.Implementation.AdminServices
{
    public class CloseCaseService : ICloseCaseService
    {
        private readonly IRequestClientRepository _requestClientRepository;
        private readonly IRequestWiseFileRepository _requestWiseFileRepository;

        public CloseCaseService(IRequestClientRepository requestClientRepository, IRequestWiseFileRepository requestWiseFileRepository)
        {
            _requestClientRepository = requestClientRepository;
            _requestWiseFileRepository = requestWiseFileRepository;
        }

        public CloseCase getDaetails(int requestId)
        {
            List<FileModel> fileList = _requestWiseFileRepository.getFilesByrequestId(requestId)
                .Select(requestWiseFile => new FileModel()
                {
                    RequestId = requestId,
                    RequestWiseFileId = requestWiseFile.RequestWiseFileId,
                    FileName = requestWiseFile.FileName,
                    Uploder = requestWiseFile.Uploder,
                    CreatedDate = requestWiseFile.CreatedDate,
                }).ToList();
            RequestClient requestClient = _requestClientRepository.getRequestClientByRequestId(requestId);
            return new CloseCase()
            {
                FirstName = requestClient.FirstName,
                LastName = requestClient.LastName,
                Phone = requestClient.PhoneNumber,
                Email = requestClient.Email,
                BirthDate = requestClient.IntDate != null ? DateTime.Parse(requestClient.IntYear + "-" + requestClient.StrMonth
                                 + "-" + requestClient.IntDate) : null,
                FileList = fileList,
            };
        }

        public async Task<bool> updateDetails(CloseCase model,int requestId)
        {
            RequestClient requestClient = _requestClientRepository.getRequestClientByRequestId(requestId);
            requestClient.FirstName = model.FirstName;
            requestClient.LastName = model.LastName;
            requestClient.PhoneNumber = model.Phone;
            requestClient.IntDate = model.BirthDate.Value.Day;
            requestClient.IntYear = model.BirthDate.Value.Year;
            requestClient.StrMonth = model.BirthDate.Value.Month.ToString();
            return await _requestClientRepository.updateRequestClient(requestClient);
        }

        public async Task<bool> requestAddToCloseCase(int requestId)
        {
            RequestClient requestClient = _requestClientRepository.getRequestClientByRequestId(requestId);
            requestClient.Status = 9;
            return await _requestClientRepository.updateRequestClient(requestClient);
        }
    }
}
