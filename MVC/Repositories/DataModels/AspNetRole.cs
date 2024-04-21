using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DataModels;

public partial class AspNetRole
{
    [Key]
    public int Id { get; set; }

    [StringLength(256)]
    public string Name { get; set; } = null!;

    [InverseProperty("Role")]
    public virtual ICollection<AspNetUserRole> AspNetUserRoles { get; set; } = new List<AspNetUserRole>();

    [InverseProperty("AccountTypeNavigation")]
    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
