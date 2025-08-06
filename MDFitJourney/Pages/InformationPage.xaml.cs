namespace MDFitJourney.Pages;

public partial class InformationPage : ContentPage
{
    public InformationPage()
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

    private async void OnSubmitFeedbackClicked(object sender, EventArgs e)
    {
        string feedback = FeedbackEditor.Text;

        if (string.IsNullOrWhiteSpace(feedback))
        {
            await DisplayAlert("Error", "Please enter your feedback before submitting.", "OK");
            return;
        }

        // Optionally, save the feedback locally for later review
        string path = Path.Combine(FileSystem.AppDataDirectory, "feedback.txt");
        File.AppendAllText(path, feedback + Environment.NewLine);

        await DisplayAlert("Thank you!", "Your feedback has been submitted.", "OK");

        FeedbackEditor.Text = string.Empty;
    }
}
