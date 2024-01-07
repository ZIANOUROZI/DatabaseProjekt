using System;
using System.Collections.Generic;

namespace DatabaseProjekt.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public int FkstaffId { get; set; }

    public int FkclassId { get; set; }

    public virtual Class Fkclass { get; set; } = null!;

    public virtual Staff Fkstaff { get; set; } = null!;

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
