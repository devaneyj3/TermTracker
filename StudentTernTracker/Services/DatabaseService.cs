using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using StudentTernTracker.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StudentTernTracker.Services
{
    public static class DatabaseService
    {

        #region Courses Methods

        public static async Task AddCouse(string name, int termId, string status, DateTime start, DateTime end, string instructorName, string instructorEmail, string instructorPhone, string notes, bool notifications)
        {
            await Init();

            var course = new Course()
            {
                Name = name,
                TermId = termId,
                Status = status,
                StartDate = start,
                EndDate = end,
                InstructorName = instructorName,
                InstructorEmail = instructorEmail,
                InstructorPhone = instructorPhone,
                Notes = notes,
                Notification = notifications
                
            };
            await _db.InsertAsync(course);
            var id = course.Id;


        }

        public static async Task RemoveCourse(int id)
        {
            await Init();

            await _db.DeleteAsync<Course>(id);
        }


        public static async Task<IEnumerable<Course>> GetCoursesByTerm(int termID)
        {
            await Init();

            var courses = await _db.Table<Course>().Where(i => i.TermId == termID).ToListAsync();
            Term.SelectedTermCourseLength = courses.Count;
            
            return courses;

        }

        public static async Task<IEnumerable<Course>> GetCourses()
        {
            await Init();
            var Courses = await _db.Table<Course>().ToListAsync();
            return Courses;
        }

        public static async Task<IEnumerable<Course>> GetCourse(int courseId)
        {
            await Init();
            var Courses = await _db.Table<Course>().Where(i => i.Id == courseId).ToListAsync();
            return Courses;
        }

        public static async Task UpdateCourse(int id, string name, string status, int termId, DateTime start, DateTime end, string instructorName, string instructorEmail, string instructorPhone, string notes, bool notifications)
        {
            await Init();

            var courseQuery = await _db.Table<Course>()
                .Where(i => i.Id == id).FirstOrDefaultAsync();

            if(courseQuery != null)
            {
                courseQuery.Name = name;
                courseQuery.TermId = termId;
                courseQuery.StartDate = start;
                courseQuery.Status = status;
                courseQuery.EndDate = end;
                courseQuery.InstructorName = instructorName;
                courseQuery.InstructorEmail = instructorEmail;
                courseQuery.InstructorPhone = instructorPhone;
                courseQuery.Notes = notes;
                courseQuery.Notification = notifications;
             
                await _db.UpdateAsync(courseQuery);
            }
        }
        #endregion

        #region Terms Methods

        public static async Task AddTerm(string name, DateTime start, DateTime end)
        {
            await Init();


            var term = new Term()
            {
                Name = name,
                StartDate = start,
                EndDate = end
            };
            await _db.InsertAsync(term);
            var id = term.Id;
        }

        public static async Task RemoveTerm(int id)
        {
            await Init();
            await _db.DeleteAsync<Term>(id);
        }

        public static async Task<IEnumerable<Term>> GetTerm(int TermId)
        {
            await Init();
            var Terms = await _db.Table<Term>().Where(i => i.Id == TermId).ToListAsync();
            return Terms;
        }
        public static async Task FindTerm(int TermId)
        {
            await Init();
            var term = await _db.Table<Term>().Where(i => i.Id == TermId).FirstOrDefaultAsync();
            
            if (term != null)
            {

                Course.SelectedTermName = term.Name;
            }
        }
        public static async Task FindTermByName(string name)
        {
            await Init();
            var term = await _db.Table<Term>().Where(i => i.Name == name).FirstOrDefaultAsync();

            if (term != null)
            {

                Term.SelectedTermId = term.Id;
            }
        }
        public static async Task<IEnumerable<Term>> GetTerms()
        {
            await Init();
            var Terms = await _db.Table<Term>().ToListAsync();
            return Terms;
        }

        public static async Task UpdateTerm(int id, string name, DateTime start, DateTime end)
        {
            await Init();

            var termQuery = await _db.Table<Term>()
                .Where(i => i.Id == id).FirstOrDefaultAsync();

            if (termQuery != null)
            {
                termQuery.Name = name;
                termQuery.StartDate = start;
                termQuery.EndDate = end;
      
                await _db.UpdateAsync(termQuery);
            }
        }


        #endregion

        #region Assesment Methods

        public static async Task AddAssesment(string name, string type, int courseID, DateTime Start, DateTime End, bool notifications)
        {
            await Init();

            var assesment = new Assesment()
            {
                Name = name,
                CourseId = courseID,
                Start = Start,
                End = End,
                Type = type,
                Notification = notifications
            };
            await _db.InsertAsync(assesment);
            var id = assesment.Id;


        }

        public static async Task RemoveAssessment(int id)
        {
            await Init();

            await _db.DeleteAsync<Assesment>(id);
        }

        public static async Task<IEnumerable<Assesment>> GetAssessmentByCourse(int courseID)
        {
            await Init();

            var assessment = await _db.Table<Assesment>().Where(i => i.CourseId == courseID).ToListAsync();
            Assesment.CourseAssementLength = assessment.Count;

            return assessment;

        }

        public static async Task<IEnumerable<Assesment>> GetAssesments()
        {
            await Init();
            var assesments = await _db.Table<Assesment>().ToListAsync();
            return assesments;
        }


        public static async Task<IEnumerable<Assesment>> GetAssesment(int assesmentID)
        {
            await Init();
            var assesments = await _db.Table<Assesment>().Where(i => i.Id == assesmentID).ToListAsync();
            return assesments;
        }

        public static async Task UpdateAssessment(int assesmentID, string name, string type, DateTime Start, DateTime End, bool notifications)
        {
            await Init();

            var courseQuery = await _db.Table<Assesment>()
                .Where(i => i.Id == assesmentID).FirstOrDefaultAsync();

            if (courseQuery != null)
            {
                courseQuery.Name = name;
                courseQuery.Type = type;
                courseQuery.Start = Start;
                courseQuery.End = End;
                courseQuery.Notification = notifications;

                await _db.UpdateAsync(courseQuery);
            }
        }
        #endregion

        public static async Task AddDummyData()
        {
            await Init();

            Term term = new Term
            {
                Id = 1,
                Name = "Fall 2023",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };


            await _db.InsertAsync(term);

            Course course = new Course
            {
                Id = 1,
                Name = "UX Design",
                TermId = term.Id,
                StartDate = DateTime.Now,
                Status = "In Progress",
                EndDate = DateTime.Now,
                InstructorName = "George",
                InstructorEmail = "Jefferson",
                InstructorPhone = "333-333-3333",
                Notes = "make wireframes",
                Notification = true
            };
            await _db.InsertAsync(course);

            Assesment OA = new Assesment
            {
                Id = 1,
                Name = "Test for UX Design",
                CourseId = course.Id,
                Start = DateTime.Now,
                Type = "OA",
                End = DateTime.Now,
                Notification = true
            };

            await _db.InsertAsync(OA);


            Assesment PA = new Assesment
            {
                Id = 2,
                Name = "Project for UX Design",
                CourseId = course.Id,
                Start = DateTime.Now,
                Type = "PA",
                End = DateTime.Now,
                Notification = true
            };

            await _db.InsertAsync(PA);
        }
        private static SQLiteAsyncConnection _db;
        private static SQLiteConnection _dbConnection;

        static async Task Init()
        {
            if(_db != null)
            {
                return;
            }
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Terms.db");

            
            _db = new SQLiteAsyncConnection(databasePath);
            _dbConnection = new SQLiteConnection(databasePath);

            await _db.CreateTableAsync<Term>();
            await _db.CreateTableAsync<Course>();
            await _db.CreateTableAsync<Assesment>();
        }
        
    }
}
