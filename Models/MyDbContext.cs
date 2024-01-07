using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DatabaseProjekt.Models;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Staff> Staffs { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Databasprojekt");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.Property(e => e.ClassName).HasMaxLength(50);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.Property(e => e.CourseName).HasMaxLength(50);
            entity.Property(e => e.FkclassId).HasColumnName("FKClassId");
            entity.Property(e => e.FkstaffId).HasColumnName("FKStaffId");

            entity.HasOne(d => d.Fkclass).WithMany(p => p.Courses)
                .HasForeignKey(d => d.FkclassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Courses_Classes");

            entity.HasOne(d => d.Fkstaff).WithMany(p => p.Courses)
                .HasForeignKey(d => d.FkstaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Courses_Staffs");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.Property(e => e.DepartmentName).HasMaxLength(50);
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.Property(e => e.FkcourseId).HasColumnName("FKCourseId");
            entity.Property(e => e.FkstudentId).HasColumnName("FKStudentId");

            entity.HasOne(d => d.Fkcourse).WithMany(p => p.Grades)
                .HasForeignKey(d => d.FkcourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Grades_Courses");

            entity.HasOne(d => d.Fkstudent).WithMany(p => p.Grades)
                .HasForeignKey(d => d.FkstudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Grades_Students");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.EmploymentDate).HasColumnName("Employment Date");
            entity.Property(e => e.Role1)
                .HasMaxLength(50)
                .HasColumnName("Role");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.FkdepartmentId).HasColumnName("FKDepartmentId");
            entity.Property(e => e.FkroleId).HasColumnName("FKRoleId");
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Salary).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Fkdepartment).WithMany(p => p.Staff)
                .HasForeignKey(d => d.FkdepartmentId)
                .HasConstraintName("FK_Staffs_Departments1");

            entity.HasOne(d => d.Fkrole).WithMany(p => p.Staff)
                .HasForeignKey(d => d.FkroleId)
                .HasConstraintName("FK_Staffs_Roles");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
