using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DataModels;

[Table("HealthProfessionalType")]
public partial class HealthProfessionalType
{
    [Key]
    public int HealthProfessionalId { get; set; }

    [StringLength(50)]
    public string ProfessionName { get; set; } = null!;

    [Column(TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [Column(TypeName = "bit(1)")]
    public BitArray? IsActive { get; set; }

    [Column(TypeName = "bit(1)")]
    public BitArray? IsDeleted { get; set; }

    [InverseProperty("ProfessionNavigation")]
    public virtual ICollection<HealthProfessional> HealthProfessionals { get; set; } = new List<HealthProfessional>();
}
