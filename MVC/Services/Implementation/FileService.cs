using Microsoft.AspNetCore.Http;
using Repositories.DataModels;
using Repositories.Interfaces;
using Services.Interfaces;
using System.Collections;

namespace Services.Implementation
{
    public class FileService : IFileService
    {
        private readonly IRequestWiseFileRepository _requestWiseFileRepository;

        public FileService(IRequestWiseFileRepository requestWiseFileRepository)
        {
            _requestWiseFileRepository = requestWiseFileRepository;
        }

        public async Task<bool> addFile(IFormFile file, int requestId, string firstName, string lastName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/"+requestId.ToString());
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            };
            FileInfo fileInfo = new FileInfo(file.FileName);
            string fileName = fileInfo.Name;
            string fileNameWithPath = Path.Combine(path, fileName);
            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            RequestWiseFile requestWiseFile = new()
            {
                RequestId = requestId,
                FileName = fileName,
                CreatedDate = DateTime.Now,
                Uploder = firstName + " " + lastName,
                IsDeleted = new BitArray(1, false),
            };
            return await _requestWiseFileRepository.addFile(requestWiseFile);
        }

    }
}
