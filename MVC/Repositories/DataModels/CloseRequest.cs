using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DataModels;

public partial class CloseRequest
{
    [Key]
    public int RequestClosedId { get; set; }

    public int RequestId { get; set; }

    public int RequestStatusLogId { get; set; }

    [StringLength(500)]
    public string? PhyNotes { get; set; }

    [StringLength(500)]
    public string? ClientNotes { get; set; }

    [Column("IP")]
    [StringLength(20)]
    public string? Ip { get; set; }

    [ForeignKey("RequestId")]
    [InverseProperty("CloseRequests")]
    public virtual Request Request { get; set; } = null!;

    [ForeignKey("RequestStatusLogId")]
    [InverseProperty("CloseRequests")]
    public virtual RequestStatusLog RequestStatusLog { get; set; } = null!;
}
