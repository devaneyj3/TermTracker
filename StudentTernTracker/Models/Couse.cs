using System;
using System.Collections.Generic;
using SQLite;
namespace StudentTernTracker.Models
{
    public class Course
    {
        public static bool Notified = false;
        public static List<string> CourseStatusList = new List<string> { "In Progress", "Completed", "Dropped", "Plan to Take" };
        public static int SelectedCourseId = 0;
        public static string SelectedCourseName = "";
        public static string SelectedTermName = null;
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int TermId { get; set; } //Foreign key for term table
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string InstructorName { get; set; }
        public string InstructorEmail { get; set; }
        public string InstructorPhone { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
    }
}
