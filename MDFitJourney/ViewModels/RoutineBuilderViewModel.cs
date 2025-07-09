using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MDFitJourney.Services;
using MDFitJourney.Pages;

namespace MDFitJourney.ViewModels
{
    public class RoutineDay : INotifyPropertyChanged
    {
        private string title;

        public string DayName { get; set; }
        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public class RoutineBuilderViewModel
    {
        public ObservableCollection<RoutineDay> Days { get; }
        public Command SaveCommand { get; }
        public Command<string> DayTappedCommand { get; }

        public RoutineBuilderViewModel()
        {
            Days = new ObservableCollection<RoutineDay>();

            foreach (var kv in RoutineService.Instance.GetAllRoutines())
            {
                Days.Add(new RoutineDay { DayName = kv.Key, Title = kv.Value });
            }

            SaveCommand = new Command(Save);

            DayTappedCommand = new Command<string>(async (dayName) =>
            {
                await Shell.Current.GoToAsync($"{nameof(RoutineEditPage)}?day={dayName}");
            });
        }

        void Save()
        {
            foreach (var day in Days)
            {
                RoutineService.Instance.SetRoutineForDay(day.DayName, day.Title);
            }
        }
    }
}
