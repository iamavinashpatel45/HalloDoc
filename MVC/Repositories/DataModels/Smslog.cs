using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DataModels;

[Table("SMSLog")]
public partial class Smslog
{
    [Key]
    [Column("SMSLogID")]
    [Precision(9, 0)]
    public decimal SmslogId { get; set; }

    [Column("SMSTemplate")]
    [StringLength(100)]
    public string Smstemplate { get; set; } = null!;

    [StringLength(50)]
    public string MobileNumber { get; set; } = null!;

    [StringLength(200)]
    public string? ConfirmationNumber { get; set; }

    public int? RoleId { get; set; }

    public int? AdminId { get; set; }

    public int? RequestId { get; set; }

    public int? PhysicianId { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime CreateDate { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime SentDate { get; set; }

    [Column("IsSMSSent", TypeName = "bit(1)")]
    public BitArray? IsSmssent { get; set; }

    public int SentTries { get; set; }

    public int? Action { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("Smslogs")]
    public virtual Role? Role { get; set; }
}
