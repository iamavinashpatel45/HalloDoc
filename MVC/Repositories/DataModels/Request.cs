using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DataModels;

[Table("Request")]
public partial class Request
{
    [Key]
    public int RequestId { get; set; }

    public int RequestTypeId { get; set; }

    public int? UserId { get; set; }

    [StringLength(100)]
    public string? FirstName { get; set; }

    [StringLength(100)]
    public string? LastName { get; set; }

    [StringLength(23)]
    public string? PhoneNumber { get; set; }

    [StringLength(50)]
    public string? Email { get; set; }

    [StringLength(20)]
    public string? ConfirmationNumber { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [Column(TypeName = "bit(1)")]
    public BitArray? IsDeleted { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? ModifiedDate { get; set; }

    [StringLength(250)]
    public string? DeclinedBy { get; set; }

    [Column(TypeName = "bit(1)")]
    public BitArray? IsUrgentEmailSent { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? LastWellnessDate { get; set; }

    [Column(TypeName = "bit(1)")]
    public BitArray? IsMobile { get; set; }

    public short? CallType { get; set; }

    [Column(TypeName = "bit(1)")]
    public BitArray? CompletedByPhysician { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? LastReservationDate { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? AcceptedDate { get; set; }

    [StringLength(100)]
    public string? RelationName { get; set; }

    [StringLength(50)]
    public string? CaseNumber { get; set; }

    [Column("IP")]
    [StringLength(20)]
    public string? Ip { get; set; }

    [StringLength(50)]
    public string? CaseTagPhysician { get; set; }

    [StringLength(128)]
    public string? PatientAccountId { get; set; }

    public int? CreatedUserId { get; set; }

    public int? CaseTagId { get; set; }

    [InverseProperty("Request")]
    public virtual ICollection<BlockRequest> BlockRequests { get; set; } = new List<BlockRequest>();

    [ForeignKey("CaseTagId")]
    [InverseProperty("Requests")]
    public virtual CaseTag? CaseTag { get; set; }

    [InverseProperty("Request")]
    public virtual ICollection<CloseRequest> CloseRequests { get; set; } = new List<CloseRequest>();

    [InverseProperty("Request")]
    public virtual ICollection<Encounter> Encounters { get; set; } = new List<Encounter>();

    [InverseProperty("Request")]
    public virtual ICollection<RequestBusiness> RequestBusinesses { get; set; } = new List<RequestBusiness>();

    [InverseProperty("Request")]
    public virtual ICollection<RequestClient> RequestClients { get; set; } = new List<RequestClient>();

    [InverseProperty("Request")]
    public virtual ICollection<RequestConcierge> RequestConcierges { get; set; } = new List<RequestConcierge>();

    [InverseProperty("Request")]
    public virtual ICollection<RequestNote> RequestNotes { get; set; } = new List<RequestNote>();

    [InverseProperty("Request")]
    public virtual ICollection<RequestStatusLog> RequestStatusLogs { get; set; } = new List<RequestStatusLog>();

    [InverseProperty("Request")]
    public virtual ICollection<RequestWiseFile> RequestWiseFiles { get; set; } = new List<RequestWiseFile>();

    [ForeignKey("UserId")]
    [InverseProperty("Requests")]
    public virtual User? User { get; set; }
}
