namespace MDFitJourney.Pages;

public partial class WeightTrackerPage : ContentPage
{
    public WeightTrackerPage()
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