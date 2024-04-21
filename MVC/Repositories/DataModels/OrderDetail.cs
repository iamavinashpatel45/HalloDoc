using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DataModels;

public partial class OrderDetail
{
    [Key]
    public int Id { get; set; }

    public int? VendorId { get; set; }

    public int? RequestId { get; set; }

    [StringLength(50)]
    public string? FaxNumber { get; set; }

    [StringLength(50)]
    public string? Email { get; set; }

    [StringLength(100)]
    public string? BusinessContact { get; set; }

    public string? Prescription { get; set; }

    public int? NoOfRefill { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? CreatedDate { get; set; }

    [StringLength(100)]
    public string? CreatedBy { get; set; }
}
