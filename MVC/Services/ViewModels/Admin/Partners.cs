using System.ComponentModel.DataAnnotations;

namespace Services.ViewModels.Admin
{
    public class Partners
    {
        public Dictionary<int,String> ProfessionList { get; set; }

        public List<PartnersTableData> PartnersTableDatas {  get; set; }
    }

    public class PartnersTableData
    {
        public int VenderId { get; set; }

        public string Profession { get; set; }

        public string BusinessName { get; set; }

        public string Email { get; set; }

        public string FaxNumber { get; set; }

        public string PhoneNumber { get; set; }

        public string BusinessContact { get; set; }
    }

    public class BusinessProfile
    {
        public string AspAction { get; set; } = "CreateBusiness";

        public bool IsUpdate { get; set; } = false;

        public Dictionary<int, String>? ProfessionList { get; set; }

        public Dictionary<int, String>? RegionList { get; set; }

        [StringLength(100)]
        public string VendorName { get; set; }

        public int Profession { get; set; }

        [StringLength(50)]
        public string FaxNumber { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        public string Street { get; set; }

        [StringLength(50)]
        public string Zip { get; set; }

        [StringLength(100)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "The         public string PhoneNumber { get; set; }\r\n is not valid")]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        [DataType(DataType.EmailAddress, ErrorMessage = "The Email is not valid")]
        public string Email { get; set; }

        [StringLength(100)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "The BusinessContact is not valid")]
        public string BusinessContact { get; set; }
    }
}
