using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DataModels;

[Table("Admin")]
public partial class Admin
{
    [Key]
    public int AdminId { get; set; }

    public int AspNetUserId { get; set; }

    [StringLength(100)]
    public string FirstName { get; set; } = null!;

    [StringLength(100)]
    public string? LastName { get; set; }

    [StringLength(50)]
    public string Email { get; set; } = null!;

    [StringLength(20)]
    public string? Mobile { get; set; }

    [StringLength(500)]
    public string? Address1 { get; set; }

    [StringLength(500)]
    public string? Address2 { get; set; }

    [StringLength(100)]
    public string? City { get; set; }

    public int? RegionId { get; set; }

    [StringLength(10)]
    public string? Zip { get; set; }

    [StringLength(20)]
    public string? AltPhone { get; set; }

    public int? CreatedBy { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? ModifiedDate { get; set; }

    public short? Status { get; set; }

    public bool? IsDeleted { get; set; }

    [StringLength(50)]
    public string? Role { get; set; }

    [InverseProperty("Admin")]
    public virtual ICollection<AdminRegion> AdminRegions { get; set; } = new List<AdminRegion>();

    [ForeignKey("AspNetUserId")]
    [InverseProperty("AdminAspNetUsers")]
    public virtual AspNetUser AspNetUser { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    [InverseProperty("AdminCreatedByNavigations")]
    public virtual AspNetUser? CreatedByNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("AdminModifiedByNavigations")]
    public virtual AspNetUser? ModifiedByNavigation { get; set; }

    [InverseProperty("Admin")]
    public virtual ICollection<RequestStatusLog> RequestStatusLogs { get; set; } = new List<RequestStatusLog>();

    [InverseProperty("Admin")]
    public virtual ICollection<RequestWiseFile> RequestWiseFiles { get; set; } = new List<RequestWiseFile>();
}
