using System;
using System.Collections.Generic;
using SQLite;

namespace StudentTernTracker.Models
{
    public class Assesment
    {
        public static bool AssessmentNotification = false;
        public static List<string> typesList = new List<string>{ "PA", "OA" };
        public static int CourseAssementLength = 0;
        public static int SelectedAssessmentID = 0;
        public static string SelectedAssesmentName = null;
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } 
        public int CourseId { get; set; } //Foreign key for term table
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Notification { get; set; }
        
    }
}

