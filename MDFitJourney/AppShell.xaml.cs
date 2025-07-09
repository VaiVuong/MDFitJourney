using MDFitJourney.Pages;

namespace MDFitJourney
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(RoutineBuilderPage), typeof(RoutineBuilderPage));
            Routing.RegisterRoute(nameof(RoutineEditPage), typeof(RoutineEditPage));
            Routing.RegisterRoute(nameof(WeightTrackerPage), typeof(WeightTrackerPage));
            Routing.RegisterRoute(nameof(InformationPage), typeof(InformationPage));
        }
    }
}
