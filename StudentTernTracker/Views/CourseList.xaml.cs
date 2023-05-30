using System;
using System.Collections.Generic;
using System.Linq;
using Plugin.LocalNotifications;
using StudentTernTracker.Models;
using StudentTernTracker.Services;
using Xamarin.Forms;

namespace StudentTernTracker.Views
{
    public partial class CourseList : ContentPage
    {
        public CourseList()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();



            var termList = await DatabaseService.GetTerms();

            TermListView.ItemsSource = termList;

            //generated notification ID's

            var courses = await DatabaseService.GetCourses();

            var courseRandom = new Random();

            int courseNotificationID = courseRandom.Next(1000);

            //notification for courses

            foreach (Course course in courses)
            {
                if (course.StartDate == DateTime.Today)
                {
                
                    CrossLocalNotifications.Current.Show("Notice", $"{course.Name} begins today and ends on {course.EndDate}", courseNotificationID);
                }
            }


            var termRandomNum = new Random();

            int termNotificationID = termRandomNum.Next(1000);

            //notification for terms
            foreach (Term term in termList)
            {
                if (term.StartDate == DateTime.Today && !Term.Notified)
                {
                    Term.Notified = true;
                    CrossLocalNotifications.Current.Show("Notice", $"{term.Name} begins today and ends on {term.EndDate}", termNotificationID);
                }
            }

            //notification for assesments
            var assessments = await DatabaseService.GetAssesments();

            var assessmentRandomNum = new Random();

            int assessmentNotificationID = assessmentRandomNum.Next(1000);

            foreach (Assesment assessment in assessments)
            {
                if (assessment.Start == DateTime.Today)
                {

                    CrossLocalNotifications.Current.Show("Notice", $"{assessment.Name} begins today and ends on {assessment.End}", assessmentNotificationID);
                }
            }


        }
        #region add handlers

        async void AddTerm_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTerm());
        }

        #endregion

        #region edit handlers

        async void EditTerm_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditTerm());
        }

        async void EditAssesment_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditAssesment());
        }
        #endregion


        async void Settings_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AppSettings());
        }



        void TermListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var term = e.SelectedItem as Term;

                Term.SelectedTermId = term.Id;

                Term.SelectedTermName = term.Name;

                Navigation.PushAsync(new TermDetail(term));

            }
            TermListView.SelectedItem = null;
        }

        async void Setting_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AppSettings());
        }
    }

}
