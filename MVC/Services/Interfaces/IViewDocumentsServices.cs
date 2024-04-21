using Services.ViewModels;

namespace Services.Interfaces
{
    public interface IViewDocumentsServices
    {
        ViewDocument getDocumentList(int requestId, int aspNetUserId);

        Task<bool> uploadFile(ViewDocument model,String firstName,String lastName,int requestId);

        Task<bool> deleteFile(int requestWiseFileId);

        Task<bool> deleteAllFile(String requestWiseFileIds, int requestId);

        bool sendFileMail(String requestWiseFileIds,int requestId);

    }
}
