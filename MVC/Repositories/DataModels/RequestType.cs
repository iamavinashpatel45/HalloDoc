using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DataModels;

[Table("RequestType")]
public partial class RequestType
{
    [Key]
    public int RequestTypeId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;
}
