using Repositories.DataContext;
using Repositories.DataModels;
using Repositories.Interfaces;

namespace Repositories.Implementation
{
    public class RequestNotesRepository : IRequestNotesRepository
    {
        private readonly HalloDocDbContext _dbContext;

        public RequestNotesRepository(HalloDocDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public RequestNote GetRequestNoteByRequestId(int requestId)
        {
            return _dbContext.RequestNotes.FirstOrDefault(a => a.RequestId == requestId);
        }

        public async Task<bool> addRequestNote(RequestNote requestNote)
        {
            _dbContext.RequestNotes.Add(requestNote);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> updateRequestNote(RequestNote requestNote)
        {
            _dbContext.RequestNotes.Update(requestNote);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
