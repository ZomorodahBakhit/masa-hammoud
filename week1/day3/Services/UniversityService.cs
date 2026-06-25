using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using university.Data;
using university.Models;

namespace university.Services
{
    public class UniversityService
    {
        private readonly AppDbContext _context;

        public UniversityService(AppDbContext context)
        {
            _context = context;
        }

        public List<Course> GetAllCourses() => _context.Courses.ToList();

        public List<Assignment> GetAssignmentsByCourse(string courseTitle)
        {
            return _context.Assignments
                .Include(a => a.Course)
                .Where(a => a.Course.Title == courseTitle)
                .ToList();
        }

        public List<User> GetAllStudents() => _context.Users.Where(u => u.Role == "Student").ToList();

        public List<Comment> GetCommentsForAssignment(int assignmentId)
        {
            return _context.Comments.Include(c => c.User).Where(c => c.AssignmentId == assignmentId).ToList();
        }

        public List<Grade> GetGradesForStudent(string studentName)
        {
            return _context.Grades
                .Include(g => g.Assignment)
                .Include(g => g.Student)
                .Where(g => g.Student.UserName == studentName)
                .ToList();
        }

        public List<Assignment> GetAssignmentsWithTeacherDetails()
        {
            return _context.Assignments
                .Include(a => a.Course)
                .ThenInclude(c => c.Teacher)
                .ToList();
        }

        public dynamic GetAverageGradePerCourse()
        {
            return _context.Courses
                .Select(c => new
                {
                    CourseTitle = c.Title,
                    Average = c.Assignments.SelectMany(a => a.Grades).Average(g => g.Score ?? 0) 
                }).ToList();
        }

        public dynamic GetStudentsWithGradesForCourse(int courseId)
        {
            return _context.Grades
                .Where(g => g.Assignment.CourseId == courseId)
                .Select(g => new
                {
                    StudentName = g.Student != null ? g.Student.UserName : "Unknown",
                    AssignmentTitle = g.Assignment != null ? g.Assignment.Title : "No Title",
                    Grade = g.Score
                })
                .ToList();
        }

        public string GetGrade(double score) => score switch
        {
            >= 90 => "A",
            >= 80 => "B",
            >= 70 => "C",
            >= 60 => "D",
            _ => "F"
        };

        public double CalculateGPA(int studentId)
        {
            var studentGrades = _context.Grades
                .Include(g => g.Assignment)
                .Where(g => g.StudentId == studentId)
                .ToList();

            if (!studentGrades.Any()) return 0.0;

            double totalWeightedScores = studentGrades
                .Sum(g => (g.Score ?? 0) * g.Assignment.Weight);

            double totalWeights = studentGrades
                .Where(g => g.Score.HasValue)
                .Sum(g => g.Assignment.Weight);

            return totalWeights > 0 ? (totalWeightedScores / totalWeights) : 0.0;
        }

        public string StudentToTeacher(string studentName)
        {
            var student = _context.Users.FirstOrDefault(u => u.UserName == studentName && u.Role == "Student");
            if (student != null)
            {
                student.Role = "Teacher";
                _context.SaveChanges();
                return $"Student '{studentName}' has been successfully promoted to a Teacher.";
            }
            return $"Student '{studentName}' was not found or is already a Teacher.";
        }

        public string DeleteCommentById(int commentId)
        {
            var comment = _context.Comments.FirstOrDefault(c => c.Id == commentId);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                _context.SaveChanges();
                return $"Comment with ID {commentId} has been deleted successfully.";
            }
            return $"Comment with ID {commentId} was not found.";
        }
    }
}