namespace Services.ViewModels
{
    public class FileModel
    {
        public int RequestWiseFileId { get; set; }

        public int RequestId { get; set; }

        public string FileName { get; set; } = null;

        public DateTime CreatedDate { get; set; }

        public string? Uploder { get; set; }

    }
}
