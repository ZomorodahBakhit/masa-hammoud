using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using university.Models;

namespace university.Data
{
    public static class DataSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (context.Users.Any()) return;

            //3. Insert Data for all the interns into the user table with Role ‘Student’ & Two teachers(Sami, Feryal)
            var user1 = new User { UserName = "MASAHAMMOUD", PhoneNumber = "0000000000", EmailAddress = "MASAH@GMAIL.COM", Role = "Student" };
            var user2 = new User { UserName = "SARAMOUHAMMAD", PhoneNumber = "1111111111", EmailAddress = "SARAH@GMAIL.COM", Role = "Student" };
            var user3 = new User { UserName = "SALMAALMASRI", PhoneNumber = "2222222222", EmailAddress = "SALMA@GMAIL.COM", Role = "Student" };
            var teacherSami = new User { UserName = "SAMIHIJAZI", PhoneNumber = "3333333333", EmailAddress = "SAMI@GMAIL.COM", Role = "Teacher" };
            var teacherFyrial = new User { UserName = "FYRIAL", PhoneNumber = "4444444444", EmailAddress = "FYRIAL@GMAIL.COM", Role = "Teacher" };

            var users = new List<User> { user1, user2, user3, teacherSami, teacherFyrial };
            context.Users.AddRange(users);
            context.SaveChanges();

            //At least 5 courses assigned to that teacher & A syllabus for each course
            var coursesToSeed = new[]
            {
                new { Title = "SQL", Teacher = teacherSami, SyllabusDesc = "SQL Course", DescPattern = "JOINS" },
                new { Title = "C#", Teacher = teacherFyrial, SyllabusDesc = "C# Course", DescPattern = "VARIABLES" },
                new { Title = "EF", Teacher = teacherSami, SyllabusDesc = "EF Course", DescPattern = "OBJECT-RELATION MAPPER" },
                new { Title = "WEB API", Teacher = teacherFyrial, SyllabusDesc = "WEB API Course", DescPattern = "API" },
                new { Title = "REACT", Teacher = teacherSami, SyllabusDesc = "REACT Course", DescPattern = "WEB APPLICATION" }            
            };
            var createdCourses = new List<Course>();


            foreach (var item in coursesToSeed)
            {
                var syllabus = new Syllabus { Description = item.SyllabusDesc };
                context.Syllabuses.Add(syllabus);

                var course = new Course
                {
                    Title = item.Title,
                    Teacher = item.Teacher, 
                    StartDate = new DateTime(2026, 6, 21),
                    EndDate = new DateTime(2026, 8, 13),
                    Syllabus = syllabus 
                };

                context.Courses.Add(course);
                createdCourses.Add(course);
            }
            context.SaveChanges();

            //At least 5 assignments per course with random due dates .
            var random = new Random();

            foreach (var course in createdCourses)
            {
                string desc = course.Title switch
                {
                    "SQL" => "JOINS",
                    "C#" => "VARIABLES",
                    "EF" => "OBJECT-RELATION MAPPER",
                    "WEB API" => "API",
                    _ => "WEB APPLICATION"
                };

                for (int assignmentN = 1; assignmentN <= 5; assignmentN++)
                {
                    context.Assignments.Add(new Assignment
                    {
                        Course = course, 
                        Title = $"{course.Title} Assignment {assignmentN}",
                        Description = desc,
                        Weight = 15,
                        MaxGrade = 100,
                        DueDate = DateTime.Now.AddDays(random.Next(-30, 30))
                    });
                }
            }
            context.SaveChanges();

            var allAssignments = context.Assignments.ToList();
            var students = context.Users.Where(u => u.Role == "Student").ToList();

            //At least 10 comments for random assignments
            string[] commentOptions = { "GOOD", "EASY", "HARD" };
            for (int i = 0; i < 10; i++)
            {
                context.Comments.Add(new Comment
                {
                    Assignment = allAssignments[random.Next(allAssignments.Count)],
                    User = students[random.Next(students.Count)],
                    Content = commentOptions[random.Next(3)],
                    CreatedAt = DateTime.Now
                });
            }
            context.SaveChanges();

            //A grade for each student per assignment
            foreach (var student in students)
            {
                foreach (var assignment in allAssignments)
                {
                    context.Grades.Add(new Grade
                    {
                        Assignment = assignment,
                        Student = student,
                        Score = random.Next(0, 101)
                    });
                }
            }
            context.SaveChanges();
        }
    }
}