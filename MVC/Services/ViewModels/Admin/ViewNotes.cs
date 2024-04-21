using System.ComponentModel.DataAnnotations;

namespace Services.ViewModels.Admin
{
    public class ViewNotes
    {
        public int RequestId { get; set; }

        [StringLength(500)]
        public string? PhysicianNotes { get; set; }

        public string? AdminNotes { get; set; }

        [StringLength(500)]
        public string NewNotes { get; set; }

        public Dictionary<String,String>? TransferNotes { get; set; }
    }
}
