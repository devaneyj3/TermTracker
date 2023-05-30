using System;
using System.Collections.Generic;
using StudentTernTracker.Models;
using StudentTernTracker.Services;
using Xamarin.Forms;

namespace StudentTernTracker.Views
{
    public partial class EditTerm : ContentPage
    {
        public EditTerm()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var TermList = await DatabaseService.GetTerm(Term.SelectedTermId);

            foreach (Term term in TermList)
            {
                //prepopulate form
                TermID.Text = term.Id.ToString();
                TermName.Text = term.Name;
                StartDatePicker.Date = term.StartDate;
                EndDatePicker.Date = term.EndDate;


            }
        }
        async void EditTerm_Clicked(object sender, EventArgs e)
        {
            //Get Values from fields
            string termName = TermName.Text;
            DateTime termStart = StartDatePicker.Date;
            DateTime termEnd = EndDatePicker.Date;

            //check if values are entered
            if (string.IsNullOrWhiteSpace(termName))
            {
                await DisplayAlert("Missing Term Name", "Please enter a term name", "OK");
                return;
            }

            if (termEnd < termStart)
            {
                await DisplayAlert("Error", "The End Date can't be before the Start Date", "OK");
                return;
            }
            await DatabaseService.UpdateTerm(Term.SelectedTermId, termName, termStart, termEnd);

            await DisplayAlert("Updated", "Term is updated", "OK");
            await Navigation.PopToRootAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
