using Services.ViewModels;

namespace Services.Interfaces.PatientServices
{
    public interface IAddRequestService
    {
        bool IsEmailExists(String email);

        AddRequestByPatient getModelForRequestByMe(int aspNetUserId);

        Task<bool> addPatientRequest(AddPatientRequest model);

        Task<bool> addRequestForMe(AddRequestByPatient model);

        Task<bool> addRequestForSomeOneelse(AddRequestByPatient model, int aspNetUserIdMe);

        Task<bool> addConciergeRequest(AddConciergeRequest model);

        Task<bool> addFamilyFriendRequest(AddFamilyRequest model);

        Task<bool> addBusinessRequest(AddBusinessRequest model);
    }
}
