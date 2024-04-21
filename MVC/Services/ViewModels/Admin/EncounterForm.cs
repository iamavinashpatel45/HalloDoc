using System.ComponentModel.DataAnnotations;

namespace Services.ViewModels.Admin
{
    public class EncounterForm
    {
        public bool IsFinalize { get; set; } = false;

        public bool IsAdmin { get; set; } = true;

        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The FirstName is not valid")]
        public string FirstName { get; set; }

        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The LastName is not valid")]
        public string LastName { get; set; }

        [StringLength(500)]
        public string Location { get; set; }

        [StringLength(50)]
        [DataType(DataType.EmailAddress, ErrorMessage = "The Email is not valid")]
        public string Email { get; set; }

        [Required]
        public DateTime? BirthDate { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        [StringLength(20)]
        [RegularExpression(@"^(?:\+?91)?\s*([1-9]\d{4})\s*(\d{5})$", ErrorMessage = "The Mobile is not valid")]
        public string Mobile { get; set; }

        [StringLength(500)]
        public string HistoryOfIllness { get; set; }

        [StringLength(500)]
        public string MedicalHistory { get; set; }

        [StringLength(500)]
        public string Medications { get; set; }

        [StringLength(500)]
        public string Allergies { get; set; }

        [StringLength(20)]
        public string Temp { get; set; }

        [StringLength(20)]
        public string HeartRate { get; set; }

        [StringLength(20)]
        public string RespiratoryRate { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "The BloodPressure field is required.")]
        public string BloodPressure1 { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "The BloodPressure field is required.")]
        public string BloodPressure2 { get; set; }

        [StringLength(20)]
        public string O2 { get; set; }

        [StringLength(500)]
        public string Pain { get; set; }

        [StringLength(500)]
        public string Heent { get; set; }

        [StringLength(500)]
        public string CV { get; set; }

        [StringLength(500)]
        public string Chest { get; set; }

        [StringLength(500)]
        public string ABD { get; set; }

        [StringLength(500)]
        public string Extra { get; set; }

        [StringLength(500)]
        public string Skin { get; set; }

        [StringLength(500)]
        public string Neuro { get; set; }

        [StringLength(500)]
        public string Other { get; set; }

        [StringLength(500)]
        public string Diagnosis { get; set; }

        [StringLength(500)]
        public string TreatmentPlan { get; set; }

        [StringLength(500)]
        public string Dispensed { get; set; }

        [StringLength(500)]
        public string Procedures { get; set; }

        [StringLength(500)]
        public string FollowUp { get; set; }
    }

    public class GeneratePdf
    {
        public Dictionary<String, String> PropertyList { get; set; }

        public EncounterForm EncounterForm { get; set; }
    }

}
