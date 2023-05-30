using System;
using System.Collections.Generic;
using StudentTernTracker.Models;
using StudentTernTracker.Services;
using StudentTernTracker.Views;
using Xamarin.Forms;

namespace StudentTernTracker
{
    public partial class TermDetail : ContentPage
    {
        public TermDetail(Term term)
        {
            if (term == null)
            {
                throw new ArgumentNullException();
            }
            InitializeComponent();

            BindingContext = term;
            TermTitle.Text = $" {term.Name}";
            TermDate.Text = $"{term.StartDate.ToShortDateString()} - {term.EndDate.ToShortDateString()}";
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var CourseList = await DatabaseService.GetCoursesByTerm(Term.SelectedTermId);
            


            CourseListView.ItemsSource = CourseList;


        }
        async void EditTerm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditTerm());
        }

        async void DeleteTerm_Clicked(object sender, EventArgs e)
        {
            if(Term.SelectedTermCourseLength == 0)
            {
                var response = await DisplayAlert("Delete", $"Do you want to delete {Term.SelectedTermName}", "Yes", "No");

                if (response == true)
                {
                    await DatabaseService.RemoveTerm(Term.SelectedTermId);

                    await Navigation.PopAsync();

                }

            } else
            {
                await DisplayAlert("Error", "You Need to Delete the Courses before the Term", "OK");
            }
        }

        async void CourseListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var course = e.SelectedItem as Course;

                Course.SelectedCourseId = course.Id;
                Course.SelectedCourseName = course.Name;
                Term.SelectedTermId = course.TermId;


                await DatabaseService.FindTerm(course.TermId);

                await Navigation.PushAsync(new CourseDetail(course));

            }
            CourseListView.SelectedItem = null;
        }

        async void AddCourse_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddCourse());
        }
    }
}
