using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DataModels;

[Table("Region")]
public partial class Region
{
    [Key]
    public int RegionId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    public string? Abbreviation { get; set; }

    [InverseProperty("Region")]
    public virtual ICollection<AdminRegion> AdminRegions { get; set; } = new List<AdminRegion>();

    [InverseProperty("Region")]
    public virtual ICollection<Business> Businesses { get; set; } = new List<Business>();

    [InverseProperty("Region")]
    public virtual ICollection<Concierge> Concierges { get; set; } = new List<Concierge>();

    [InverseProperty("Region")]
    public virtual ICollection<PhysicianRegion> PhysicianRegions { get; set; } = new List<PhysicianRegion>();

    [InverseProperty("Region")]
    public virtual ICollection<RequestClient> RequestClients { get; set; } = new List<RequestClient>();

    [InverseProperty("Region")]
    public virtual ICollection<ShiftDetailRegion> ShiftDetailRegions { get; set; } = new List<ShiftDetailRegion>();

    [InverseProperty("Region")]
    public virtual ICollection<ShiftDetail> ShiftDetails { get; set; } = new List<ShiftDetail>();
}
