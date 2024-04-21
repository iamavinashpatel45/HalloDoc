using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DataModels;

[Table("PhysicianNotification")]
public partial class PhysicianNotification
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    public int PhysicianId { get; set; }

    [Column(TypeName = "bit(1)")]
    public BitArray IsNotificationStopped { get; set; } = null!;

    [ForeignKey("PhysicianId")]
    [InverseProperty("PhysicianNotifications")]
    public virtual Physician Physician { get; set; } = null!;
}
