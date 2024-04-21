using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DataModels;

[Table("Shift")]
public partial class Shift
{
    [Key]
    public int ShiftId { get; set; }

    public int PhysicianId { get; set; }

    public DateOnly StartDate { get; set; }

    [Column(TypeName = "bit(1)")]
    public BitArray IsRepeat { get; set; } = null!;

    [StringLength(7)]
    public string? WeekDays { get; set; }

    public int? RepeatUpto { get; set; }

    public int CreatedBy { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [Column("IP")]
    [StringLength(20)]
    public string? Ip { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("Shifts")]
    public virtual AspNetUser CreatedByNavigation { get; set; } = null!;

    [ForeignKey("PhysicianId")]
    [InverseProperty("Shifts")]
    public virtual Physician Physician { get; set; } = null!;

    [InverseProperty("Shift")]
    public virtual ICollection<ShiftDetail> ShiftDetails { get; set; } = new List<ShiftDetail>();
}
