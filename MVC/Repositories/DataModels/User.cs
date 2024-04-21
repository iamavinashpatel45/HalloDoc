using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DataModels;

[Table("User")]
public partial class User
{
    [Key]
    public int UserId { get; set; }

    public int? AspNetUserId { get; set; }

    [StringLength(100)]
    public string FirstName { get; set; } = null!;

    [StringLength(100)]
    public string? LastName { get; set; }

    [StringLength(50)]
    public string Email { get; set; } = null!;

    [StringLength(20)]
    public string? Mobile { get; set; }

    [Column(TypeName = "bit(1)")]
    public BitArray? IsMobile { get; set; }

    [StringLength(100)]
    public string? Street { get; set; }

    [StringLength(100)]
    public string? City { get; set; }

    [StringLength(100)]
    public string? State { get; set; }

    public int? RegionId { get; set; }

    [StringLength(10)]
    public string? ZipCode { get; set; }

    [Column("strMonth")]
    [StringLength(20)]
    public string? StrMonth { get; set; }

    [Column("intYear")]
    public int? IntYear { get; set; }

    [Column("intDate")]
    public int? IntDate { get; set; }

    public int CreatedBy { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? ModifiedDate { get; set; }

    public short? Status { get; set; }

    [Column(TypeName = "bit(1)")]
    public BitArray? IsDeleted { get; set; }

    [Column("IP")]
    [StringLength(20)]
    public string? Ip { get; set; }

    [Column(TypeName = "bit(1)")]
    public BitArray? IsRequestWithEmail { get; set; }

    [Column("house")]
    [StringLength(100)]
    public string? House { get; set; }

    [ForeignKey("AspNetUserId")]
    [InverseProperty("UserAspNetUsers")]
    public virtual AspNetUser? AspNetUser { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("UserCreatedByNavigations")]
    public virtual AspNetUser CreatedByNavigation { get; set; } = null!;

    [ForeignKey("ModifiedBy")]
    [InverseProperty("UserModifiedByNavigations")]
    public virtual AspNetUser? ModifiedByNavigation { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
