using System;
using System.Collections.Generic;

namespace DatabaseProjekt.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public decimal Salary { get; set; }

    public int? FkdepartmentId { get; set; }

    public int? FkroleId { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual Department? Fkdepartment { get; set; }

    public virtual Role? Fkrole { get; set; }
}
