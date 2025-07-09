using MDFitJourney.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDFitJourney.ViewModels;

public class MainPageViewModel
{
    public string CurrentDay { get; set; } = DateTime.Now.DayOfWeek.ToString();
    public string CurrentRoutine { get; set; } = "Chest / Tricep";

    public Command GoToRoutineBuilderCommand { get; }
    public Command GoToWeightTrackerCommand { get; }
    public Command OpenMenuCommand { get; }
    public Command AddWeightCommand { get; }
    public Command GoToInfoCommand { get; }

    public MainPageViewModel()
    {
        GoToRoutineBuilderCommand = new Command(async () =>
        {
            await Shell.Current.GoToAsync(nameof(RoutineBuilderPage));
        });

        GoToWeightTrackerCommand = new Command(async () =>
        {
            await Shell.Current.GoToAsync(nameof(WeightTrackerPage));
        });

        OpenMenuCommand = new Command(() =>
        {
            // future humburger button
        });

        AddWeightCommand = new Command(async () =>
        {
            await Shell.Current.GoToAsync(nameof(WeightTrackerPage));
        });

        GoToInfoCommand = new Command(async () =>
        {
            await Shell.Current.GoToAsync(nameof(InformationPage));
        });
    }
}
