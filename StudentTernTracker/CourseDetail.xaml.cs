using System;
using System.Collections.Generic;
using StudentTernTracker.Models;
using StudentTernTracker.Services;
using StudentTernTracker.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StudentTernTracker
{
    public partial class CourseDetail : ContentPage
    {
         public CourseDetail(Course course)
        {
            if(course == null)
            {
                throw new ArgumentNullException();
            }
            InitializeComponent();

            
            BindingContext = course;

            //option for sharing if there is notes

            if(!string.IsNullOrWhiteSpace(course.Notes))
            {
                ShareBtn.IsVisible = true;
                
            }
            else
            {
                NotesLabel.Text = "No Notes";
                ShareBtn.IsVisible = false;
               
            }
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();

            //populate term label
            var label = new Label();


            label.Text = Course.SelectedTermName;
            GridView.Children.Add(label, 1, 9);

            //get course assesments
            var assessments = await DatabaseService.GetAssessmentByCourse(Course.SelectedCourseId);

            AssessmentListView.ItemsSource = assessments;

        }

        async void EditCourse_OnClicked(object sender, EventArgs e)
        {
           
            await Navigation.PushAsync(new EditCourse());
        }

        async void AddAssesment_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddAssesment());
        }

        async void DeleteCourse_Clicked(object sender, EventArgs e)
        {
            if (Assesment.CourseAssementLength == 0)
            {
                var response = await DisplayAlert("Delete", $"Do you want to delete {Course.SelectedCourseName}", "Yes", "No");

                if (response == true)
                {
                    await DatabaseService.RemoveCourse(Course.SelectedCourseId);

                    await Navigation.PopAsync();

                }
            }
            else
            {
                await DisplayAlert("Error", "You need to delete the assessments before the course", "OK");
            }
        }

       async void AssessmentListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var assessment = e.SelectedItem as Assesment;
            Assesment.SelectedAssessmentID = assessment.Id;
            Assesment.SelectedAssesmentName = assessment.Name;
            await Navigation.PushAsync(new AssesmentDetail(assessment));
        }

       async void Button_Clicked(object sender, EventArgs e)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Title = "Share Notes"
            });
        }
    }
}
