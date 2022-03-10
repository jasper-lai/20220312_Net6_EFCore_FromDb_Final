using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyConsoleApp.Models.Database;
using Microsoft.EntityFrameworkCore;
using MyConsoleApp.Models;
using MyConsoleApp.ViewModels;

namespace MyConsoleApp.Services
{
    internal class StudentGradeService
    {
        //private readonly SchoolContext _db = new();
        private readonly ConsoleSchoolContext _db = new();

        public async Task<IEnumerable<StudentGradeViewModel>> GetStudentGrades()
        {
            var query = _db.StudentGrades
                .Where(x => x.Grade == 3)
                .Select(x => new StudentGradeViewModel()
                {
                    EnrollmentId = x.EnrollmentId,
                    CourseId = x.CourseId,
                    StudentId = x.StudentId,
                    Grade = x.Grade,
                    StudentName = x.Student.FirstName + " " + x.Student.LastName,
                    CourseName = x.Course.Title,
                    CourseCredits = x.Course.Credits,
                });

            Console.WriteLine($"thread: {Environment.CurrentManagedThreadId} --> before 存取資料庫 ----");
            var result = await query.ToListAsync();
            Console.WriteLine($"thread: {Environment.CurrentManagedThreadId} --> after 存取資料庫 ----");

            return result;
        }
    }
}
