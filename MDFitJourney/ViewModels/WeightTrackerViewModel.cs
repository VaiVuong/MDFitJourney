using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System;

namespace MDFitJourney.ViewModels
{
    public partial class WeightEntry : ObservableObject
    {
        [ObservableProperty]
        private DateTime date;

        [ObservableProperty]
        private double weight;
    }

    public partial class WeightTrackerViewModel : ObservableObject
    {
        [ObservableProperty]
        private string currentWeight;

        [ObservableProperty]
        private DateTime selectedDate = DateTime.Today;

        public ObservableCollection<WeightEntry> WeightEntries { get; set; } = new ObservableCollection<WeightEntry>();


        [RelayCommand]
        private void SaveWeight()
        {
            if (double.TryParse(CurrentWeight, out double weightValue))
            {
                var existingEntry = WeightEntries.FirstOrDefault(e => e.Date.Date == SelectedDate.Date);

                if (existingEntry != null)
                {
                    existingEntry.Weight = weightValue;
                }
                else
                {
                    var newEntry = new WeightEntry
                    {
                        Date = SelectedDate.Date,
                        Weight = weightValue
                    };
                    WeightEntries.Add(newEntry);
                }

                var sortedEntries = WeightEntries.OrderBy(e => e.Date).ToList();
                WeightEntries.Clear();
                foreach (var entry in sortedEntries)
                {
                    WeightEntries.Add(entry);
                }

                CurrentWeight = string.Empty;
                SelectedDate = DateTime.Today;
            }
            else
            {
                Console.WriteLine("Invalid weight format. Please enter a number.");
            }
        }

        [RelayCommand]
        private void EditWeight(WeightEntry entry)
        {
            if (entry != null)
            {
                SelectedDate = entry.Date;
                CurrentWeight = entry.Weight.ToString();
            }
        }
    }
}