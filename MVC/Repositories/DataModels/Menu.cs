using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DataModels;

[Table("Menu")]
public partial class Menu
{
    [Key]
    public int MenuId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public short AccountType { get; set; }

    public int? SortOrder { get; set; }

    [InverseProperty("Menu")]
    public virtual ICollection<RoleMenu> RoleMenus { get; set; } = new List<RoleMenu>();
}
