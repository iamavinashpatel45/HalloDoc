using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DataModels;

[Table("Encounter")]
public partial class Encounter
{
    [Key]
    public int EncounterId { get; set; }

    public int? RequestId { get; set; }

    [StringLength(100)]
    public string? FirstName { get; set; }

    [StringLength(100)]
    public string? LastName { get; set; }

    [StringLength(500)]
    public string? Location { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? Date { get; set; }

    [Column("strMonth")]
    [StringLength(20)]
    public string? StrMonth { get; set; }

    [Column("intYear")]
    public int? IntYear { get; set; }

    [Column("intDate")]
    public int? IntDate { get; set; }

    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    [StringLength(50)]
    public string? Email { get; set; }

    [StringLength(500)]
    public string? MedicalHistory { get; set; }

    [StringLength(500)]
    public string? IllnessOrInjury { get; set; }

    [StringLength(500)]
    public string? Medications { get; set; }

    [StringLength(500)]
    public string? Allergies { get; set; }

    [StringLength(20)]
    public string? Temperature { get; set; }

    [StringLength(20)]
    public string? HeartRate { get; set; }

    [StringLength(20)]
    public string? RespiratoryRate { get; set; }

    [StringLength(20)]
    public string? BloodPressure1 { get; set; }

    [StringLength(20)]
    public string? O2 { get; set; }

    [StringLength(500)]
    public string? Pain { get; set; }

    [Column("HEENT")]
    [StringLength(500)]
    public string? Heent { get; set; }

    [Column("CV")]
    [StringLength(500)]
    public string? Cv { get; set; }

    [StringLength(500)]
    public string? Chest { get; set; }

    [Column("ABD")]
    [StringLength(500)]
    public string? Abd { get; set; }

    [StringLength(500)]
    public string? Extr { get; set; }

    [StringLength(500)]
    public string? Skin { get; set; }

    [StringLength(500)]
    public string? Neuro { get; set; }

    [StringLength(500)]
    public string? Other { get; set; }

    [StringLength(500)]
    public string? Diagnosis { get; set; }

    [StringLength(500)]
    public string? TreatmentPlan { get; set; }

    [StringLength(500)]
    public string? MedicationsDispensed { get; set; }

    [StringLength(500)]
    public string? Procedures { get; set; }

    [StringLength(500)]
    public string? Followup { get; set; }

    public bool? IsFinalize { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? FinalizeDate { get; set; }

    public int? FinalizeBy { get; set; }

    [StringLength(128)]
    public string? ModifiedBy { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? ModifiedDate { get; set; }

    [StringLength(20)]
    public string? BloodPressure2 { get; set; }

    [ForeignKey("RequestId")]
    [InverseProperty("Encounters")]
    public virtual Request? Request { get; set; }
}
