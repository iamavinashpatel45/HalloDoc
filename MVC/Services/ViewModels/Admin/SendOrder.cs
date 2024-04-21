using System.ComponentModel.DataAnnotations;

namespace Services.ViewModels.Admin
{
    public class SendOrder
    {
        public int RequestId { get; set; }

        public int NoOfRefill { get; set; }

        public Dictionary<int, String>? Professions { get; set; }

        [Display(Name = "Profession")]
        public int SelectedProfession { get; set; }

        [Display(Name = "Business")]
        public int SelectedBusiness { get; set; }

        [StringLength(100)]
        public String Contact { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string FaxNumber { get; set; }

        public string OrderDetails { get; set; }

    }
}
