using MDFitJourney.Pages;
using MDFitJourney.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MDFitJourney.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private string currentDay = DateTime.Now.DayOfWeek.ToString();

        public event PropertyChangedEventHandler PropertyChanged;

        public string CurrentDay
        {
            get => currentDay;
            set
            {
                if (currentDay != value)
                {
                    currentDay = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CurrentRoutine)); // Update routine when day changes
                }
            }
        }

        public string CurrentRoutine
        {
            get
            {
                var routineDay = RoutineService.Instance.GetRoutineForDay(CurrentDay);
                return routineDay?.Title ?? "No Routine Set";
            }
        }

        public ICommand GoToRoutineBuilderCommand { get; }
        public ICommand GoToWeightTrackerCommand { get; }
        public ICommand GoToHomeCommand { get; }
        public ICommand AddWeightCommand { get; }
        public ICommand GoToInfoCommand { get; }

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

            GoToHomeCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync(nameof(MainPage));
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

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
