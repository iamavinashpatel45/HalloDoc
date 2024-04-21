using Services.ViewModels.Admin;

namespace Services.Interfaces.AdminServices
{
    public interface ICloseCaseService
    {
        CloseCase getDaetails(int requestId);

        Task<bool> updateDetails(CloseCase model, int requestId);

        Task<bool> requestAddToCloseCase(int requestId);
    }
}
