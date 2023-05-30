using System;
using System.Collections.Generic;
using StudentTernTracker.Models;
using StudentTernTracker.Services;
using Xamarin.Forms;

namespace StudentTernTracker.Views
{
    public partial class EditCourse : ContentPage
    {
        public EditCourse()
        {
            InitializeComponent();

            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //populate term picker
            var nameList = new List<string>();
            var TermList = await DatabaseService.GetTerms();

            foreach (Term term in TermList)
            {
                nameList.Add(term.Name);
            }

            TermPicker.ItemsSource = nameList;

            StatusPicker.ItemsSource = Course.CourseStatusList;


            //populate course details
            var courseList = await DatabaseService.GetCourse(Course.SelectedCourseId);
        

            foreach (Course course in courseList)
            {
                //prepopulate form
                CourseID.Text = course.Id.ToString();
                CourseName.Text = course.Name;
                InstructorName.Text = course.InstructorName;
                InstructorEmail.Text = course.InstructorEmail;
                InstructorPhone.Text = course.InstructorPhone;
                TermPicker.SelectedItem = Term.SelectedTermName;
                StatusPicker.SelectedItem = course.Status;
                start.Date = course.StartDate;
                end.Date = course.EndDate;
                Notes.Text = course.Notes;

            }
        }
            async void EditCourse_Clicked(object sender, EventArgs e)
        {
            var item = TermPicker.SelectedItem.ToString();

            await DatabaseService.FindTermByName(item);
            //Get Values from fields
            string courseName = CourseName.Text;
            string courseInstructorName = InstructorName.Text;
            string courseInstructorEmail = InstructorEmail.Text;
            string courseInstructorPhone = InstructorPhone.Text;
            int couseTermPicker = Term.SelectedTermId;
            DateTime courseStartDate = start.Date;
            DateTime courseEndDate = end.Date;
            string courseNotes = Notes.Text;
            string status = StatusPicker.SelectedItem.ToString();

            //check if values are entered
            if (string.IsNullOrWhiteSpace(courseName))
            {
                await DisplayAlert("Missing Name", "Please enter a name", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(courseInstructorName))
            {
                await DisplayAlert("Missing Instructor Name", "Please enter the instructor name", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(courseInstructorEmail))
            {
                await DisplayAlert("Missing Email", "Please enter the instructor email", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(courseInstructorPhone))
            {
                await DisplayAlert("Missing Phone Number", "Please enter the instructor phone number", "OK");
                return;
            }

            if (courseEndDate < courseStartDate)
            {
                await DisplayAlert("Error", "The End Date can't be before the Start Date", "OK");
                return;
            }

            await DatabaseService.UpdateCourse(Course.SelectedCourseId, courseName, status, couseTermPicker, courseStartDate, courseEndDate, courseInstructorName, courseInstructorEmail, courseInstructorPhone, courseNotes);

            await DisplayAlert("Updated", "Course is updated", "OK");
            await Navigation.PopToRootAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
