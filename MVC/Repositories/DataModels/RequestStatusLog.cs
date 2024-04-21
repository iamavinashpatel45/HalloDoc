using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DataModels;

[Table("RequestStatusLog")]
public partial class RequestStatusLog
{
    [Key]
    public int RequestStatusLogId { get; set; }

    public int RequestId { get; set; }

    public short? Status { get; set; }

    public int? PhysicianId { get; set; }

    public int? AdminId { get; set; }

    public int? TransToPhysicianId { get; set; }

    [StringLength(500)]
    public string? Notes { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [Column("IP")]
    [StringLength(20)]
    public string? Ip { get; set; }

    [Column(TypeName = "bit(1)")]
    public BitArray? TransToAdmin { get; set; }

    [ForeignKey("AdminId")]
    [InverseProperty("RequestStatusLogs")]
    public virtual Admin? Admin { get; set; }

    [InverseProperty("RequestStatusLog")]
    public virtual ICollection<CloseRequest> CloseRequests { get; set; } = new List<CloseRequest>();

    [ForeignKey("PhysicianId")]
    [InverseProperty("RequestStatusLogPhysicians")]
    public virtual Physician? Physician { get; set; }

    [ForeignKey("RequestId")]
    [InverseProperty("RequestStatusLogs")]
    public virtual Request Request { get; set; } = null!;

    [ForeignKey("TransToPhysicianId")]
    [InverseProperty("RequestStatusLogTransToPhysicians")]
    public virtual Physician? TransToPhysician { get; set; }
}
