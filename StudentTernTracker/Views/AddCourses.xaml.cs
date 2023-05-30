using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudentTernTracker.Models;
using StudentTernTracker.Services;
using Xamarin.Forms;

namespace StudentTernTracker.Views
{

    public partial class AddCourse : ContentPage
    {

        public AddCourse()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var nameList = new List<string>();
            var TermList = await DatabaseService.GetTerms();

            foreach (Term term in TermList)
            {
                
                nameList.Add(term.Name);
            }

            TermPicker.ItemsSource = nameList;
            TermPicker.SelectedItem = Term.SelectedTermName;

            StatusPicker.ItemsSource = Course.CourseStatusList;
            StatusPicker.SelectedItem = Course.CourseStatusList[3].ToString();
        }
        async void SaveCourse_Clicked(object sender, EventArgs e)
        {

            var item = TermPicker.SelectedItem.ToString();

            await DatabaseService.FindTermByName(item);

            string Name = CourseName.Text;
            int TermId = Term.SelectedTermId;
            DateTime StartDate = start.Date;
            DateTime EndDate = end.Date;
            string instructorName = InstructorName.Text;
            string instructorEmail = InstructorEmail.Text;
            string instructorPhone = InstructorPhone.Text;
            string notes = Notes.Text;
            string status = StatusPicker.SelectedItem.ToString();
            bool notification = Notification.IsToggled;

            //check if values are entered
            if(string.IsNullOrWhiteSpace(Name))
            {
                await DisplayAlert("Missing Name", "Please enter a name", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(instructorName))
            {
                await DisplayAlert("Missing Instructor Name", "Please enter the instructor name", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(instructorEmail))
            {
                await DisplayAlert("Missing Email", "Please enter the instructor email", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(instructorPhone))
            {
                await DisplayAlert("Missing Phone Number", "Please enter the instructor phone number", "OK");
                return;
            }

            if (EndDate < StartDate)
            {
                await DisplayAlert("Error", "The End Date can't be before the Start Date", "OK");
                return;
            }

            await DatabaseService.AddCouse(Name, TermId, status, StartDate, EndDate, instructorName, instructorPhone, instructorEmail, notes, notification);

                await Navigation.PopAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}
