using Repositories.DataModels;

namespace Repositories.Interfaces
{
    public interface IRequestNotesRepository
    {
        RequestNote GetRequestNoteByRequestId(int requestId);

        Task<bool> addRequestNote(RequestNote requestNote);

        Task<bool> updateRequestNote(RequestNote requestNote);
    }
}
