using MDFitJourney.Pages;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace MDFitJourney
{
    public partial class AppShell : Shell
    {
        public ICommand GoToHomeCommand { get; }
        public ICommand AddWeightCommand { get; }
        public ICommand GoToWeightTrackerCommand { get; }
        public ICommand GoToInfoCommand { get; }

        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(RoutineBuilderPage), typeof(RoutineBuilderPage));
            Routing.RegisterRoute(nameof(WeightTrackerPage), typeof(WeightTrackerPage));
            Routing.RegisterRoute(nameof(InformationPage), typeof(InformationPage));

            GoToHomeCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(MainPage)));
            AddWeightCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(WeightTrackerPage)));
            GoToWeightTrackerCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(WeightTrackerPage)));
            GoToInfoCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(InformationPage)));

            BindingContext = this;
        }
    }
}
