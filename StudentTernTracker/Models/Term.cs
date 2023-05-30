using System;
using SQLite;
namespace StudentTernTracker.Models
{
    public class Term
    {
        public static bool Notified = false;
        public static int SelectedTermId = 0;
        public static string SelectedTermName = "";
        public static int SelectedTermCourseLength = 0;
       [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


    }
}
