using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DataModels;

[Table("RequestClient")]
public partial class RequestClient
{
    [Key]
    public int RequestClientId { get; set; }

    public int RequestId { get; set; }

    [StringLength(100)]
    public string FirstName { get; set; } = null!;

    [StringLength(100)]
    public string? LastName { get; set; }

    [StringLength(23)]
    public string? PhoneNumber { get; set; }

    [StringLength(100)]
    public string? Location { get; set; }

    [StringLength(500)]
    public string? Address { get; set; }

    public int? RegionId { get; set; }

    [StringLength(20)]
    public string? NotiMobile { get; set; }

    [StringLength(50)]
    public string? NotiEmail { get; set; }

    [StringLength(50)]
    public string? Email { get; set; }

    [Column("strMonth")]
    [StringLength(20)]
    public string? StrMonth { get; set; }

    [Column("intYear")]
    public int? IntYear { get; set; }

    [Column("intDate")]
    public int? IntDate { get; set; }

    [Column(TypeName = "bit(1)")]
    public BitArray? IsMobile { get; set; }

    [StringLength(100)]
    public string? Street { get; set; }

    [StringLength(100)]
    public string? City { get; set; }

    [StringLength(100)]
    public string? State { get; set; }

    [StringLength(10)]
    public string? ZipCode { get; set; }

    public short? CommunicationType { get; set; }

    public short? RemindReservationCount { get; set; }

    public short? RemindHouseCallCount { get; set; }

    public short? IsSetFollowupSent { get; set; }

    [Column("IP")]
    [StringLength(20)]
    public string? Ip { get; set; }

    public short? IsReservationReminderSent { get; set; }

    [Precision(9, 6)]
    public decimal? Latitude { get; set; }

    [Precision(9, 6)]
    public decimal? Longitude { get; set; }

    public int Status { get; set; }

    [StringLength(500)]
    public string? Symptoms { get; set; }

    public int? PhysicianId { get; set; }

    [ForeignKey("PhysicianId")]
    [InverseProperty("RequestClients")]
    public virtual Physician? Physician { get; set; }

    [ForeignKey("RegionId")]
    [InverseProperty("RequestClients")]
    public virtual Region? Region { get; set; }

    [ForeignKey("RequestId")]
    [InverseProperty("RequestClients")]
    public virtual Request Request { get; set; } = null!;
}
