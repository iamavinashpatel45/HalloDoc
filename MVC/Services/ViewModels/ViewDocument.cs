using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Services.ViewModels
{
    public class ViewDocument
    { 
        public List<FileModel>? FileList { get; set; }

        [Required(ErrorMessage ="File Is Required")]
        public IFormFile File { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }
    }
}
