using System;
using StudentTernTracker.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentTernTracker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var courseList = new CourseList();
            var navPage = new NavigationPage(courseList);
            MainPage = navPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
