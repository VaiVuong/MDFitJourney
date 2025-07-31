using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MDFitJourney.Services;

namespace MDFitJourney.ViewModels
{
    public class RoutineDay : INotifyPropertyChanged
    {
        private bool isEditing;
        private bool isExpanded;
        private string title;

        public string DayName { get; set; }

        public string Title
        {
            get => title;
            set { title = value; OnPropertyChanged(); }
        }

        public bool IsEditing
        {
            get => isEditing;
            set
            {
                if (isEditing != value)
                {
                    isEditing = value;
                    OnPropertyChanged();

                    foreach (var exercise in Exercises)
                    {
                        exercise.IsEditing = value;
                    }
                }
            }
        }

        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                if (isExpanded != value)
                {
                    isExpanded = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<ExerciseModule> Exercises { get; } = new ObservableCollection<ExerciseModule>();

        public Command ToggleEditCommand { get; }

        public RoutineDay()
        {
            ToggleEditCommand = new Command(() =>
            {
                IsEditing = !IsEditing;
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }

    public class RoutineBuilderViewModel
    {
        public ObservableCollection<RoutineDay> Days { get; }
        public Command SaveCommand { get; }
        public Command<string> DayTappedCommand { get; }
        public Command<RoutineDay> AddExerciseCommand { get; }

        public RoutineBuilderViewModel()
        {
            Days = new ObservableCollection<RoutineDay>();

            // Load all routines including exercises
            foreach (var kv in RoutineService.Instance.GetAllRoutines())
            {
                var routineDay = new RoutineDay
                {
                    DayName = kv.Key,
                    Title = kv.Value.Title
                };

                // Load exercises from service's RoutineDay to the ViewModel's RoutineDay
                foreach (var exercise in kv.Value.Exercises)
                {
                    routineDay.Exercises.Add(new ExerciseModule
                    {
                        ExerciseName = exercise.Name,
                        Sets = exercise.Sets,
                        Reps = exercise.Reps,
                        Weight = exercise.Weight
                    });
                }

                Days.Add(routineDay);
            }

            SaveCommand = new Command(Save);

            DayTappedCommand = new Command<string>((dayName) =>
            {
                foreach (var day in Days)
                {
                    day.IsExpanded = day.DayName == dayName ? !day.IsExpanded : false;
                }
            });

            AddExerciseCommand = new Command<RoutineDay>((day) =>
            {
                day.Exercises.Add(new ExerciseModule());
            });
        }

        void Save()
        {
            foreach (var day in Days)
            {
                var serviceRoutineDay = RoutineService.Instance.GetRoutineForDay(day.DayName);

                serviceRoutineDay.Title = day.Title;

                serviceRoutineDay.Exercises.Clear();

                foreach (var exerciseModule in day.Exercises)
                {
                    serviceRoutineDay.Exercises.Add(new Exercise
                    {
                        Name = exerciseModule.ExerciseName,
                        Sets = exerciseModule.Sets,
                        Reps = exerciseModule.Reps,
                        Weight = exerciseModule.Weight
                    });
                }

                RoutineService.Instance.SetRoutineForDay(day.DayName, serviceRoutineDay);
            }
        }
    }
}
