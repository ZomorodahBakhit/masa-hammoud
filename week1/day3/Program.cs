using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using university.Data;
using university.Services;

namespace university
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new AppDbContext())
            {
                DataSeeder.Seed(context);

                var universityService = new UniversityService(context);

                //List all courses
                var courses = universityService.GetAllCourses();
                foreach (var c in courses)
                {
                    Console.WriteLine($"{c.Id}\t{c.Title}\t{c.TeacherId}\t{c.StartDate}\t{c.EndDate}");
                }

                //Show all assignments for a specific course
                var assignments = universityService.GetAssignmentsByCourse("SQL");
                foreach (var a in assignments)
                {
                    Console.WriteLine($"{a.Id}\t{a.CourseId}\t{a.Title}\t{a.Description}\t{a.Weight}\t{a.MaxGrade}\t{a.DueDate}");
                }

                //List all students
                var students = universityService.GetAllStudents();
                foreach (var s in students)
                {
                    Console.WriteLine($"{s.Id}\t{s.UserName}\t{s.EmailAddress}\t{s.Role}");
                }

                //Show all comments for a given assignment
                var targetAssignment = context.Assignments.FirstOrDefault();
                if (targetAssignment != null)
                {
                    var assignmentComments = universityService.GetCommentsForAssignment(targetAssignment.Id);
                    foreach (var cm in assignmentComments)
                    {
                        Console.WriteLine($"{cm.Content} by {cm.User.UserName}");
                    }
                }

                //Show all grades for a student
                var studentGrades = universityService.GetGradesForStudent("MASAHAMMOUD");
                foreach (var g in studentGrades)
                {
                    Console.WriteLine($"Assignment: {g.Assignment.Title} | Score: {g.Score}");
                }

                //List each assignment with its course and the teacher’s full name
                var assignmentsWithTeachers = universityService.GetAssignmentsWithTeacherDetails();
                foreach (var a in assignmentsWithTeachers)
                {
                    Console.WriteLine($"Assignment: {a.Title} | Course: {a.Course.Title} | Teacher: {a.Course.Teacher.UserName}");
                }

                //Query to show average grade per course
                var courseAverages = universityService.GetAverageGradePerCourse();
                foreach (var ca in courseAverages)
                {
                    Console.WriteLine($"{ca.CourseTitle}\t{ca.Average}");
                }

                // method to return letter grades (A, B, C, etc.) based on the student’s performance
                Console.WriteLine(universityService.GetGrade(95));

                //Create a method to calculate GPA for a student
                var firstStudent = context.Users.FirstOrDefault(u => u.Role == "Student");
                if (firstStudent != null)
                {
                    Console.WriteLine($"GPA for {firstStudent.UserName}: {universityService.CalculateGPA(firstStudent.Id)}");
                }

                //Update a student’s role to “Teacher”
                Console.WriteLine(universityService.StudentToTeacher("MASAHAMMOUD"));

                //Delete a specific comment
                Console.WriteLine(universityService.DeleteCommentById(1));

            }

        }
    }
}