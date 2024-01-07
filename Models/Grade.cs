using System;
using System.Collections.Generic;

namespace DatabaseProjekt.Models;

public partial class Grade
{
    public int GradeId { get; set; }

    public int? GradeName { get; set; }

    public DateOnly DateTime { get; set; }

    public int FkstudentId { get; set; }

    public int FkcourseId { get; set; }

    public virtual Course Fkcourse { get; set; } = null!;

    public virtual Student Fkstudent { get; set; } = null!;
}
