using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace MDFitJourney.Pages
{
    public partial class RoutineBuilderPage : ContentPage
    {
        public RoutineBuilderPage()
        {
            InitializeComponent();
        }
        private async void GoToHome_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(MainPage));
        }

        private async void AddWeight_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(WeightTrackerPage));
        }

        private async void GoToWeightTracker_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(WeightTrackerPage));
        }

        private async void GoToInfo_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(InformationPage));
        }
    }

    public class BoolToEditTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "Done" : "Edit";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
