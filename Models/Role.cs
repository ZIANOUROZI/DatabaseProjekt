using System;
using System.Collections.Generic;

namespace DatabaseProjekt.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string Role1 { get; set; } = null!;

    public DateOnly EmploymentDate { get; set; }

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
