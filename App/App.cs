using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseProjekt.Models;
using DatabaseProjekt.Models.DTOS;
using Microsoft.EntityFrameworkCore;

namespace DatabaseProjekt.App
{
    internal class App
    {
        //Mehod for all staffs information
        public void AllStaffsInfo()
    {
            MyDbContext context = new MyDbContext();
            var staffs = context.Staffs.Include(s => s.Fkrole).Include(d => d.Fkdepartment).ToList();
            List<StaffDTO> StaffList = new List<StaffDTO>();
            foreach(var staff in staffs)
            {
                DateTime currentTime = DateTime.Now;
                int daysEmployed = 0;
                string role = "none";
                if(staff.Fkrole != null  )
                {
                    daysEmployed = currentTime.Day - staff.Fkrole.EmploymentDate.Day;
                    role = staff.Fkrole.Role1;

                };
                StaffList.Add(new StaffDTO
                {
                    FirstName = staff.FirstName,
                    LastName = staff.LastName,
                    Salary = staff.Salary,
                    Role = role,
                    DaysEmployeed = daysEmployed 
                });
            }
            foreach(var staff in StaffList)
            {
                Console.WriteLine( " Förnamn: " + staff.FirstName + " Efternamn: "
                    + staff.LastName + " Lön: " + staff.Salary + " AnsälningsDatum: " + staff.DaysEmployeed + " Role: " + staff.Role);
                
            }
            Console.ReadKey();
    }
        //This method used for add a new staffs
    public void SavNewStaffs()
        {
            MyDbContext context = new MyDbContext();
            Console.WriteLine(" Ange förnamn på den nya personal:");
            string FirstName = Console.ReadLine();
            Console.WriteLine(" Ange efternamn på den nya personal:");
            string LastName = Console.ReadLine();
            Console.WriteLine(" Ange lön på den nya personal:");
            decimal salary = decimal.Parse(Console.ReadLine());
            
                var newStaff = new Staff
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Salary = salary,
                };
                context.Staffs.Add(newStaff);
                context.SaveChanges();
                Console.WriteLine($"Ny personal tillagd: {newStaff.FirstName}{newStaff.LastName} {newStaff.Salary}");
            Console.ReadKey();
        }
        //This method is for add a new students
        public void SaveAllStudents()
        {
            MyDbContext context = new MyDbContext();
            Console.WriteLine("Ange förnman på den nya eleven:");
            string FirstName = Console.ReadLine();

            Console.WriteLine("Ange efternamn för den nya eleven:");
            string LastName = Console.ReadLine();

            var newStudent = new Student
            {
                FirstName = FirstName,
                LastName = LastName
            };
            context.Students.Add(newStudent);
            context.SaveChanges();

            Console.WriteLine($"Ny elev tillagd: {newStudent.FirstName} (Personnummer: {newStudent.LastName})");
            Console.ReadKey();
        }
        //This method is used to add new grades
        public void SaveAllGrades()
        {
            MyDbContext context = new MyDbContext();

            Console.WriteLine("Ange Betyg:");
            int grade;
            while (!int.TryParse(Console.ReadLine(), out grade))
            {
                Console.WriteLine("Ogiltigt betyg. Försök igen.");
            }

            Console.WriteLine("Ange Datum (YYYY-MM-DD):");
            DateOnly date;
            while (!DateOnly.TryParse(Console.ReadLine(), out date))
            {
                Console.WriteLine("Ogiltigt datumformat. Använd YYYY-MM-DD. Försök igen.");
            }

            Console.WriteLine("Ange KursId:");
            int courseId;
            while (!int.TryParse(Console.ReadLine(), out courseId))
            {
                Console.WriteLine("Ogiltigt kursid. Försök igen.");
            }
            var studentsWithoutGrade = context.Students
                .Where(s => !context.Grades.Any(g => g.FkcourseId == courseId && g.FkstudentId == s.StudentId))
                .ToList();
            foreach (var student in studentsWithoutGrade)
            {
                Console.WriteLine($"StudentId: {student.StudentId}, Namn: {student.FirstName} {student.LastName}");
            }

            Console.WriteLine("Välj vilken elev som ska få betyg:");
            Console.ReadKey();
        }

        //Method to se how much teachers working i that departement
        public void GetTeacherCountsByDepartment()
        {
            MyDbContext context = new MyDbContext();

            var query = from d in context.Departments
                        join s in context.Staffs on d.DepartmentId equals s.FkdepartmentId into deptStaffs
                        from ds in deptStaffs.DefaultIfEmpty()
                        group ds by d.DepartmentName into grouped
                        select new
                        {
                            DepartmentName = grouped.Key,
                            TeacherCount = grouped.Count(s => s != null && s.StaffId != null)
                        };
            foreach (var result in query)
            {
                Console.WriteLine($" {result.DepartmentName} {result.TeacherCount}");
            }
            Console.ReadKey();
        }

        // That method is for all information about all students
        public void GettInfoAboutAllStudents()
        {
            MyDbContext context = new MyDbContext();
            var teachers = context.Students.FromSqlRaw("SELECT * FROM Students").ToList();
            foreach (var info in teachers)
            {
                Console.WriteLine($" StudentId: {info.StudentId} Firstname: {info.FirstName} Efternamn: {info.LastName}");
            }
            Console.ReadKey();
        }
        // Mehod for all active courses
        public void GettAllActiceCourses()
        {
            MyDbContext context = new MyDbContext();
            var courses = context.Courses.FromSqlRaw("SELECT * FROM Courses").ToList();
            foreach(var course in courses)
            {
                Console.WriteLine("Kurs namn: " + course.CourseName);
            }
            Console.ReadKey();
        }
        //Method for salary for department
        public void GetAllDepartmentSalary()
        {
            MyDbContext context = new MyDbContext();
            var salaries = context.Staffs.FromSqlRaw("SELECT [StaffId], [FirstName], [LastName], [Salary], [FKDepartmentId], [FKRoleId]\r" +
                "FROM [Databasprojekt].[dbo].[Staffs];").ToList();

            foreach (var salary in salaries)
            {
                Console.WriteLine($"Lön för {salary.FirstName} {salary.LastName} i avdelning {salary.FkdepartmentId}: {salary.Salary}");
            }
            Console.ReadKey();
        }
        //Method for average salary for departement
        public void GetAllAverageSalary()
        {
            MyDbContext context = new MyDbContext();
            var max = context.Staffs.Average(g => g.Salary);
            Console.WriteLine($" Medellönen: {max}");
            Console.ReadKey();
        }
    }
}
