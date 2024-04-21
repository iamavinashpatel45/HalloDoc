using Microsoft.AspNetCore.Http;

namespace Services.Interfaces
{
    public interface IFileService
    {
        Task<bool> addFile(IFormFile file, int requestId, string firstName, string lastName);
    }
}
