using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DataModels;

public partial class HealthProfessional
{
    [Key]
    public int VendorId { get; set; }

    [StringLength(100)]
    public string VendorName { get; set; } = null!;

    public int Profession { get; set; }

    [StringLength(50)]
    public string FaxNumber { get; set; } = null!;

    [StringLength(150)]
    public string? Address { get; set; }

    [StringLength(100)]
    public string? City { get; set; }

    [StringLength(50)]
    public string? State { get; set; }

    [StringLength(50)]
    public string? Zip { get; set; }

    public int? RegionId { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? ModifiedDate { get; set; }

    [StringLength(100)]
    public string? PhoneNumber { get; set; }

    [Column(TypeName = "bit(1)")]
    public BitArray? IsDeleted { get; set; }

    [Column("IP")]
    [StringLength(20)]
    public string? Ip { get; set; }

    [StringLength(50)]
    public string? Email { get; set; }

    [StringLength(100)]
    public string? BusinessContact { get; set; }

    [ForeignKey("Profession")]
    [InverseProperty("HealthProfessionals")]
    public virtual HealthProfessionalType ProfessionNavigation { get; set; } = null!;
}
